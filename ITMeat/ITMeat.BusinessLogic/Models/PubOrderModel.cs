using System;

namespace ITMeat.BusinessLogic.Models
{
    public class PubOrderModel : BaseModel
    {
        public OrderModel Order { get; set; }

        public PubModel Pub { get; set; }
    }
}