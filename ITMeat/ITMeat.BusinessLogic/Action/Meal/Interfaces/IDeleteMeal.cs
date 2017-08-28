using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.Meal.Interfaces
{
    public interface IDeleteMeal : IAction
    {
        bool Invoke(Guid mealId);
    }
}