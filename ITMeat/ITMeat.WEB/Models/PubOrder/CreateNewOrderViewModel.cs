using System;
using System.ComponentModel.DataAnnotations;

namespace ITMeat.WEB.Models.PubOrder
{
    public class CreateNewOrderViewModel
    {
        [Required]
        public DateTime EndOrders { get; set; }

        public string PubId { get; set; }
    }
}