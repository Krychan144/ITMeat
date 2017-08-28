using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.MealType.Interfaces
{
    public interface IDeleteMealType : IAction
    {
        bool Invoke(Guid mealTypeId);
    }
}