using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITMeat.WEB.Models.Pub;

namespace ITMeat.WEB.Models.Order
{
    public class CreateNewOrderViewModel
    {
        [Required]
        public DateTime EndOrders { get; set; }

        public string PubName { get; set; }

        public string Adress { get; set; }
    }
}