using System;
using System.Collections.Generic;
using System.Text;

namespace ITMeat.BusinessLogic.Models
{
    public class OrderModel : BaseModel
    {
        public List<OrderMealModel> OrdersPubMeals { get; set; }

        public decimal Expense { get; set; }

        public DateTime SubmitOrderDate { get; set; }
    }
}