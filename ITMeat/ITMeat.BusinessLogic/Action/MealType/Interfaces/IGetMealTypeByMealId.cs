﻿using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.MealType.Interfaces
{
    public interface IGetMealTypeByMealId : IAction
    {
        List<MealTypeModel> Invoke(Guid mealId);
    }
}