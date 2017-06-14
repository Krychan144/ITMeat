using ITMeat.WEB.Controler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}