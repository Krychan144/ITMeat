namespace ITMeat.BusinessLogic.Models
{
    public class UserOrderMealModel : BaseModel

    {
        public UserOrderModel UserOrder { get; set; }

        public MealModel PubMeal { get; set; }

        public int Quantity { get; set; }
    }
}