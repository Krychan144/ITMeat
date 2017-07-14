using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.WEB.Models.Pub;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITMeat.WEB.Models.Order
{
    public class AddNewOrderViewModel
    {
        public CreateNewOrderViewModel CreateModel { get; set; }

        public List<SelectListItem> Pubs { get; set; }
    }
}