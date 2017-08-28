using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ITMeat.DataAccess.Models;

namespace ITMeat.BusinessLogic.Models
{
    public class UserOrderAddsModel : BaseModel
    {
        public UserOrderModel UserOrder { get; set; }

        public AddsModel AddsModel { get; set; }

        public int Quantity { get; set; }
    }
}