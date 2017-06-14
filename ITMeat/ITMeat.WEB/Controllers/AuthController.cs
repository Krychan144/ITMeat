using ITMeat.BusinessLogic.Configuration.Implementations;
using ITMeat.WEB.Controler;
using ITMeat.WEB.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.WEB.Controllers
{
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            var userModel = new UserModel { Name = registerViewModel.Name, }
        }
    }
}