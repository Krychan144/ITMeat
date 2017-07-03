using System.Linq;
using ITMeat.BusinessLogic.Action.Email.Interfaces;
using ITMeat.BusinessLogic.Configuration.Implementations;
using ITMeat.WEB.Controler;
using ITMeat.WEB.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITMeat.BusinessLogic.Models;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models.Common;
using ITMeat.WEB.v;

namespace ITMeat.WEB.Controllers
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IAddNewUser _addNewUser;
        private readonly IConfirmUserEmailByToken _confirmUserEmailByToken;
        private readonly IEmailService _emailService;
        private readonly IAddNewEmailMessage _addNewEmailMessage;

        public AuthController(IAddNewUser addNewUser,
            IConfirmUserEmailByToken confirmUserEmailByToken,
            IEmailService emailService,
            IAddNewEmailMessage addNewEmailMessage
            )
        {
            _addNewUser = addNewUser;
            _confirmUserEmailByToken = confirmUserEmailByToken;
            _emailService = emailService;
            _addNewEmailMessage = addNewEmailMessage;
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Login()
        {
            return View();
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
            var emailinfo = new EmailBodyHelper().GetRegisterEmailBodyModel(callbackUrl);
            var stringView = RenderViewToString<EmailBodyModel>("ConfirmEmail", emailinfo);
            var message = _emailService.CreateMessage(model.Email, "Confirm your account", stringView);
            var mappedMessage = AutoMapper.Mapper.Map<EmailMessageModel>(message);
            _addNewEmailMessage.Invoke(mappedMessage);

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
    }
}