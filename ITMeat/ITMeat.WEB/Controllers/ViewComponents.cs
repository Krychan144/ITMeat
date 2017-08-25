using ITMeat.WEB.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace ITMeat.WEB.Controllers
{
    public class DeleteModalViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DeleteModalViewModel model)
        {
            return View(model);
        }
    }
}