using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITMeat.WEB.Models.PubOrder
{
    public class AddNewOrderViewModel
    {
        [Required]
        public DateTime EndOrders { get; set; }

        public Guid PubId { get; set; }

        public List<SelectListItem> Pubs { get; set; }
    }
}