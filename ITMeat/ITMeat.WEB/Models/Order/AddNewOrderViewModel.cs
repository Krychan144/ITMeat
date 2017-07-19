using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITMeat.WEB.Models.Order
{
    public class AddNewOrderViewModel
    {
        [Required]
        public DateTime EndOrders { get; set; }

        public Guid PubId { get; set; }

        public List<SelectListItem> Pubs { get; set; }
    }
}