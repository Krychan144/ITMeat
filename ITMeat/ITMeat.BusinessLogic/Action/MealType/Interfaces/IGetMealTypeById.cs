using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;
using System;

namespace ITMeat.BusinessLogic.Action.MealType.Interfaces
{
    public interface IGetMealTypeById : IAction
    {
        MealTypeModel Invoke(Guid mealtypeId);
    }
}