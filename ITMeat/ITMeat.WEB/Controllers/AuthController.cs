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
using ITMeat.BusinessLogic.Action.UserToken.Implementations;
using ITMeat.BusinessLogic.Action.UserToken.Interfaces;
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
        private readonly IConfirmUserEmailByToken _confirmUserEmailByToken;
        private readonly IEmailService _emailService;
        private readonly IAddNewEmailMessage _addNewEmailMessage;
        private readonly IGetUserTokenByUserId _getUserTokenByUserId;
        private readonly IGetUserByEmail _getUserByEmail;
        private readonly IAddUserTokenToUser _addUserTokenToUser;
        private readonly IGetUserByToken _getUserByToken;
        private readonly IEditUserPassword _editUserPassword;
        private readonly IAuthenticateUser _authenticateUser;

        public AuthController(IAddNewUser addNewUser,
            IConfirmUserEmailByToken confirmUserEmailByToken,
            IEmailService emailService,
            IAddNewEmailMessage addNewEmailMessage,
            IGetUserByEmail getUserByEmail,
            IAddUserTokenToUser addUserTokenToUser,
            IGetUserTokenByUserId getUserTokenByUserId,
            IGetUserByToken getUserByToken,
            IEditUserPassword editUserPassword,
            IAuthenticateUser authenticateUser)
        {
            _addNewUser = addNewUser;
            _confirmUserEmailByToken = confirmUserEmailByToken;
            _emailService = emailService;
            _addNewEmailMessage = addNewEmailMessage;
            _getUserByEmail = getUserByEmail;
            _addUserTokenToUser = addUserTokenToUser;
            _getUserTokenByUserId = getUserTokenByUserId;
            _getUserByToken = getUserByToken;
            _editUserPassword = editUserPassword;
            _authenticateUser = authenticateUser;
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
                // TODO: MOVE TO CONST STRING
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Claims");
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.Authentication.SignInAsync("Cookies", claimsPrinciple);

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "User");
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

            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { token = userAddAction.Tokens.FirstOrDefault().SecretToken },
                Request.Scheme);

            // var emailinfo = new EmailBodyHelper().GetRegisterEmailBodyModel(callbackUrl);
            // var stringView = RenderViewToString<EmailBodyModel>("ConfirmEmail", emailinfo);
            // var message = _emailService.CreateMessage(model.Email, "Confirm your account", stringView);
            // var mappedMessage = AutoMapper.Mapper.Map<EmailMessageModel>(message);
            //_addNewEmailMessage.Invoke(mappedMessage);

            Alert.Success("Confirmation email has been sent to your email address");

            return RedirectToAction("Login", "Auth");
        }

        [AllowAnonymous]
        [HttpGet("confirmemail")]
        public IActionResult ConfirmEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Alert.Danger("Invalid token");

                return View("Error");
            }

            var confirmAction = _confirmUserEmailByToken.Invoke(token);

            if (!confirmAction)
            {
                Alert.Danger("Couldn't finish this action");

                return RedirectToAction("Login", "Auth");
            }

            Alert.Success("Email confirmed");
            return RedirectToAction("Login", "Auth");
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public IActionResult ResetPasswordInitiation(ResetPasswordInitiationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Alert.Danger("Something went wrong");

                return View(model);
            }

            var user = _getUserByEmail.Invoke(model.Email);

            if (user == null)
            {
                Alert.Danger("Something went wrong");

                return View();
            }

            var token = _addUserTokenToUser.Invoke(user.Id);
            var callbackUrl = Url.Action("ResetPasswordByToken", "Auth", new { token },
                Request.Scheme);
            var emailinfo = new EmailBodyHelper().GetResetPasswordBodyModel(callbackUrl);
            var stringView = RenderViewToString<EmailBodyModel>("ResetPassword", emailinfo);
            var message = _emailService.CreateMessage(model.Email, "Confirm your account", stringView);
            var mappedMessage = AutoMapper.Mapper.Map<EmailMessageModel>(message);

            _addNewEmailMessage.Invoke(mappedMessage);
            Alert.Success("Email will be sent to your account shortly");

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpPost("resendconfirmationemail")]
        public IActionResult ResendConfirmationEmail(ResendConfirmationEmailViewModel model)
        {
            var user = _getUserByEmail.Invoke(model.Email);

            if (user == null)
            {
                Alert.Danger("Something went wrong");

                return View(model);
            }

            if (user.EmailConfirmedOn != null)
            {
                Alert.Danger("Email already confirmed");

                return RedirectToAction("Login");
            }

            var token = _addUserTokenToUser.Invoke(user.Id);

            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { token }, Request.Scheme);
            var emailinfo = new EmailBodyHelper().GetRegisterEmailBodyModel(callbackUrl);
            var stringView = RenderViewToString<EmailBodyModel>("ConfirmEmail", emailinfo);
            var message = _emailService.CreateMessage(model.Email, "Confirm your account", stringView);
            var mappedMessage = AutoMapper.Mapper.Map<EmailMessageModel>(message);

            _addNewEmailMessage.Invoke(mappedMessage);
            Alert.Success("Check your inbox");

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet("resetpasswordbytoken/{token}")]
        public IActionResult ResetPasswordByToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Alert.Danger("Invalid token");

                return View("Error");
            }

            var user = _getUserByToken.Invoke(token);

            if (user == null)
            {
                Alert.Danger("Invalid token");

                return View("Error");
            }

            var model = new ResetPasswordViewModel()
            {
                Token = token
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost("resetpasswordbytoken")]
        public IActionResult ResetPasswordByToken(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Alert.Danger("Something went wrong");

                return View(model);
            }

            var user = _getUserByToken.Invoke(model.Token);

            if (user == null)
            {
                Alert.Danger("You can't complete this action");

                return View("Login");
            }

            var result = _editUserPassword.Invoke(user.Id, model.Password);

            if (!result)
            {
                Alert.Danger("Something went wrong");

                return View(model);
            }

            Alert.Success("Your password has been updated");

            return RedirectToAction("Login");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");

            Alert.Success("Logged out");

            return RedirectToAction("Index", "Home");
        }
    }
}