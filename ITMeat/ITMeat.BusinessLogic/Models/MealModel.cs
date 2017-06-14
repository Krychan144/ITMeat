using ITMeat.DataAccess.Models;

namespace ITMeat.BusinessLogic.Models
{
    public class MealModel : BaseModel
    {
        public UserOrder UserOrder { get; set; }

        public string Name { get; set; }

        public decimal Expense { get; set; }

        public string Type { get; set; }
    }
}