using System;

namespace ITMeat.WEB.Models.Meal.FormModels
{
    public class AddNewMealToOrderViewModel
    {
        public Guid OrderId { get; set; }

        public Guid User { get; set; }

        public byte Quantity { get; set; }

        public Guid MealId { get; set; }
    }
}