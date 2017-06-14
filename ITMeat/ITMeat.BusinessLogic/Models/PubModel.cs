using System.Collections.Generic;

namespace ITMeat.BusinessLogic.Models
{
    public class PubModel : BaseModel
    {
        public List<MealModel> Meals { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }
    }
}