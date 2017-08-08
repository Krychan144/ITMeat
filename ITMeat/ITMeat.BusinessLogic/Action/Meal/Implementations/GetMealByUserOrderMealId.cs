using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class GetMealByUserOrderMealId : IGetMealByUserOrderMealId
    {
        private readonly IMealRepository _mealRepository;

        public GetMealByUserOrderMealId(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public MealModel Invoke(Guid userOrderMealid)
        {
            if (userOrderMealid == Guid.Empty)
            {
                return null;
            }

            var dbMeal = _mealRepository.GetMealByUserOrderMealId(userOrderMealid);
            if (dbMeal == null)
            {
                return null;
            }

            var Meal = AutoMapper.Mapper.Map<MealModel>(dbMeal);

            return Meal;
        }
    }
}