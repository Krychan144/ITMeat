using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces
{
    public interface IGetSumExpenseByMealTypeAndUserId : IAction
    {
        List<MealExpenseSumModel> Invoke(Guid userId);
    }
}