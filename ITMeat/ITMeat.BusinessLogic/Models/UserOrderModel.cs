using System.Collections.Generic;

namespace ITMeat.BusinessLogic.Models
{
    public class UserOrderModel : BaseModel
    {
        public UserModel User { get; set; }

        public OrderModel Order { get; set; }

        public decimal Expense { get; set; }
    }
}