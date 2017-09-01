using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;
using ITMeat.BusinessLogic.Models.AditionalsModels;

namespace ITMeat.BusinessLogic.Action.MealType.Interfaces
{
    public interface IGetMealCountByMealType : IAction
    {
        List<MealCountForAllUsersModel> Invoke();
    }
}