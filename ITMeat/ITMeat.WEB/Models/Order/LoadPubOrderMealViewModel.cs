using System;

namespace ITMeat.WEB.Models.Order
{
    public class LoadPubOrderMealViewModel
    {
        public Guid PubId { get; set; }

        public Guid MealId { get; set; }

        public string MealName { get; set; }

        public Decimal Expense { get; set; }

        public Guid TypeMealId { get; set; }
    }
}