using System.Collections.Generic;
using ITMeat.DataAccess.Models;

namespace ITMeat.BusinessLogic.Models
{
    public class PubModel : BaseModel
    {
        public List<MealModel> Meals { get; set; }

        public List<PubOrder> PubOrder { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public decimal Phone { get; set; }
    }
}