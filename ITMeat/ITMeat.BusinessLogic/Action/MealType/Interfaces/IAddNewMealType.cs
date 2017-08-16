using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.MealType.Interfaces
{
    public interface IAddNewMealType : IAction
    {
        Guid Invoke(MealTypeModel mealType);
    }
}