using Microsoft.AspNetCore.Mvc;

namespace ITMeat.WEB.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}