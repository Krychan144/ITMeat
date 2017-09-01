using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models.AditionalsModels;
using ITMeat.DataAccess.Models.Aditionals;

namespace ITMeat.BusinessLogic.Action.Meal.Interfaces
{
    public interface IGetMealCountForAllUsers : IAction
    {
        List<MostlySelectedMealInOrderModel> Invoke();
    }
}