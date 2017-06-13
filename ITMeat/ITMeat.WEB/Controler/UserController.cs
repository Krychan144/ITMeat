using Microsoft.AspNetCore.Mvc;

namespace ITMeat.WEB.Controler
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}