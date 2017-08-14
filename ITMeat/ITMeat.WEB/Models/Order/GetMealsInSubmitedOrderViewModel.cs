using System;

namespace ITMeat.WEB.Models.Order
{
    public class GetMealsInSubmitedOrderViewModel
    {
        public string UserName { get; set; }

        public Guid UserId { get; set; }

        public Guid OwnerOrderId { get; set; }

        public string MealName { get; set; }

        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public decimal Expense { get; set; }
    }
}