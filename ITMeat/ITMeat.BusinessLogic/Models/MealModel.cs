namespace ITMeat.BusinessLogic.Models
{
    public class MealModel : BaseModel
    {
        public string Name { get; set; }

        public decimal Expense { get; set; }

        public string Type { get; set; }

        public decimal Phone { get; set; }

        public PubModel Pub { get; set; }
    }
}