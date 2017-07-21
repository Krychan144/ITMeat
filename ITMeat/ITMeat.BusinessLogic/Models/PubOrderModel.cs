using System;

namespace ITMeat.BusinessLogic.Models
{
    public class PubOrderModel : BaseModel
    {
        public OrderModel Order { get; set; }

        public PubModel Pub { get; set; }

        public UserModel Owner { get; set; }

        public DateTime EndDateTime { get; set; }

        public DateTime SubmitDateTime { get; set; }
    }
}