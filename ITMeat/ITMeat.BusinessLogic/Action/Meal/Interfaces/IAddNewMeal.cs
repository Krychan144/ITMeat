using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;
using Org.BouncyCastle.Security;

namespace ITMeat.BusinessLogic.Action.Meal.Interfaces
{
    public interface IAddNewMeal : IAction
    {
        Guid Invoke(MealModel meal, Guid pubId);
    }
}