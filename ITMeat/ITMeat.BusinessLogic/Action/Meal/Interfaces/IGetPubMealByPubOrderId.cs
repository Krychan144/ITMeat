using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Meal.Interfaces
{
    public interface IGetPubMealByPubOrderId : IAction
    {
        List<MealModel> Invoke(Guid PubOrderId);
    }
}