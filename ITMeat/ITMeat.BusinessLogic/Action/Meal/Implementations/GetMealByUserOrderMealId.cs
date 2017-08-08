using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class GetMealByUserOrderMealId : IGetMealByUserOrderMealIdcs
    {
        private readonly IMealRepository _mealRepository;

        public GetMealByUserOrderMealId(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }
    }
}