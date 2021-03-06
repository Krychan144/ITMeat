﻿using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Pub.Interfaces
{
    public interface IGetPubByMealId : IAction
    {
        PubModel Invoke(Guid mealId);
    }
}