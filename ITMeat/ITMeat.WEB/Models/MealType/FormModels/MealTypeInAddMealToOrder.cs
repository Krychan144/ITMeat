using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.MealType.FormModels
{
    public class MealTypeInAddMealToOrder
    {
        public Guid MealTypeId { get; set; }

        public string MealTypeName { get; set; }
    }
}