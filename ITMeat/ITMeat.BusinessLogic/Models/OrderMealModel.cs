namespace ITMeat.BusinessLogic.Models
{
    public class OrderMealModel : BaseModel
    {
        public OrderModel Order { get; set; }

        public MealModel PubMeal { get; set; }
    }
}