using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.DataAccess.Models.Aditionals;
using ITMeat.WEB.Models.Meal;

namespace ITMeat.WEB.Models.MealType
{
    public class GroupSatysticViewModel
    {
        public List<SumExpenseByMealType> SumExpenseByMealTypesModels { get; set; }

        public List<MealsCountByMealTypeViewModel> MealsCountByMealTypeModels { get; set; }

        public List<MostlySelectedMealInOrderViewModel> MostlySelectedMealInOrder { get; set; }
    }
}