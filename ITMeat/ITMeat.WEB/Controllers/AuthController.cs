using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Email.Interfaces;
using ITMeat.BusinessLogic.Configuration.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITMeat.BusinessLogic.Models;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models;
using ITMeat.WEB.Models.Common;
using ITMeat.WEB.Models.Auth;

namespace ITMeat.WEB.Controllers
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IAddNewUser _addNewUser;
        private readonly IEmailService _emailService;
        private readonly IAddNewEmailMessage _addNewEmailMessage;
        private readonly IGetUserByEmail _getUserByEmail;
        private readonly IEditUserPassword _editUserPassword;
        private readonly IAuthenticateUser _authenticateUser;
        private readonly IChangePassword _changePassword;

        public AuthController(IAddNewUser addNewUser,
            IEmailService emailService,
            IAddNewEmailMessage addNewEmailMessage,
            IGetUserByEmail getUserByEmail,
            IEditUserPassword editUserPassword,
            IAuthenticateUser authenticateUser, IChangePassword changePassword)
        {
            _addNewUser = addNewUser;
            _emailService = emailService;
            _addNewEmailMessage = addNewEmailMessage;
            _getUserByEmail = getUserByEmail;
            _editUserPassword = editUserPassword;
            _authenticateUser = authenticateUser;
            _changePassword = changePassword;
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
                ViewBag.ReturnUrl = returnUrl;

                return View(model);
            }

            var access = _authenticateUser.Invoke(model.Email, model.Password);

            if (access == null)
            {
                ModelState.AddModelError("Email", "Invalid email or password");
                Alert.Warning();
                ViewBag.ReturnUrl = returnUrl;

                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, access.Name),
                new Claim(ClaimTypes.Actor, access.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Claims");
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.Authentication.SignInAsync("Cookies", claimsPrinciple);
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Order");
            }

            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            var userModel = new UserModel { Email = model.Email, Password = model.Password, Name = model.Name };
            var userAddAction = _addNewUser.Invoke(userModel);

            if (userAddAction == null)
            {
                Alert.Danger("User already exists");

                return View();
            }

            Alert.Success("Confirmation email has been sent to your email address");

            return RedirectToAction("Login", "Auth");
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");

            Alert.Success("Logged out");

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (model.NewPassword != model.NewPasswordConfirm)
            {
                Alert.Danger("Pasword and confirm pasword must be same.");
                return View();
            }

            if (model.NewPassword == model.OldPassword)
            {
                Alert.Danger("New Pasword and Old Pasword they can not be the same");
                return View();
            }
            var changePasswordAction = _changePassword.Invoke(model.Email, model.OldPassword, model.NewPassword,
                ControllerContext.HttpContext.Actor());
            if (changePasswordAction == false)
            {
                Alert.Danger("Error. Paswort are not change.");
                return View();
            }

            Alert.Success(); ("Success. Paswort change.");
            return View();
        }
    }
}