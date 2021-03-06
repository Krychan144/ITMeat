﻿using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class GetMealById : IGetMealById
    {
        private readonly IMealRepository _mealRepository;

        public GetMealById(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public MealModel Invoke(Guid mealId)
        {
            if (mealId == Guid.Empty)
            {
                return null;
            }
            var dbMeal = _mealRepository.GetMealbyId(mealId);
            if (dbMeal == null)
            {
                return null;
            }
            var meal = AutoMapper.Mapper.Map<MealModel>(dbMeal);

            return meal;
        }
    }
}