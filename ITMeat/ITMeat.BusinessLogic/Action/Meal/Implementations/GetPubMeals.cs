using System;
using System.Collections.Generic;
using System.Linq;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class GetPubMeals : IGetPubMeals
    {
        private readonly IMealRepository _mealRepository;

        public GetPubMeals(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public List<MealModel> Invoke(Guid pubId)
        {
            if (pubId == Guid.Empty)
            {
                return null;
            }
            var dbMeals = _mealRepository.GetPubMeals(pubId).ToList();

            if (dbMeals == null)
            {
                return null;
            }

            var mealsList = dbMeals.Select(item => new MealModel()
            {
                Id = item.Id,
                Expense = item.Expense,
                Name = item.Name,
                Type = item.Type
            }).ToList();

            return mealsList;
        }
    }
}