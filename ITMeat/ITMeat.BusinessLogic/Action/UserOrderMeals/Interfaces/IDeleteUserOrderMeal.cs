using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces
{
    public interface IDeleteUserOrderMeal : IAction
    {
        bool Invoke(Guid userOrderMealId);
    }
}