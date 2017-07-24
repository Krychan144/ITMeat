using System;

namespace ITMeat.WEB.Models.Order
{
    public class AddMealToOrder
    {
        public Guid mealId { get; set; }

        public Guid PubId { get; set; }

        public Guid User { get; set; }

        public byte Quantity { get; set; }

        public string MealName { get; set; }
    }
}