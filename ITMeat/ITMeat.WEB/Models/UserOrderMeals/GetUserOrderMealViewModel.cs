using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.UserOrderMeals
{
    public class GetUserOrderMealViewModel
    {
        public string UserName { get; set; }

        public Guid UserId { get; set; }

        public string MealName { get; set; }

        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public decimal Expense { get; set; }
    }
}