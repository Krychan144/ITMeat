using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Meal.Interfaces
{
    public interface IGetMealByUserOrderMealIdcs : IAction
    {
        MealModel Invoke(Guid userOrderMealId);
    }
}