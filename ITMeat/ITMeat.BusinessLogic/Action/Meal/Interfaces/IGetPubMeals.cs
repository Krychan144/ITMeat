using System;
using System.Collections.Generic;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Meal.Interfaces
{
    public interface IGetPubMeals : IAction
    {
        List<MealModel> Invoke(Guid pubId);
    }
}