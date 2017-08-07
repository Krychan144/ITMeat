using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces
{
    public interface IGetUserOrderMeals : IAction
    {
        List<UserOrderMealModel> Invoke(Guid orderId);
    }
}