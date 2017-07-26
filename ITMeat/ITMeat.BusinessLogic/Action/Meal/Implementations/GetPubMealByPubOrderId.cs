using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    internal class GetPubMealByPubOrderId : IGetPubMealByPubOrderId
    {
        private readonly IMealRepository _mealRepository;

        public GetPubMealByPubOrderId(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public List<MealModel> Invoke(Guid pubOrderId)
        {
            if (pubOrderId == Guid.Empty)
            {
                return null;
            }
            var dbMeals = _mealRepository.GetPubMealByPubOrderId(pubOrderId).ToList();

            if (dbMeals == null)
            {
                return null;
            }

            var mealsList = dbMeals.Select(item => new MealModel()
            {
                Id = item.Id,
                Expense = item.Expense,
                Name = item.Name,
                Pub = new PubModel
                {
                    Id = item.Pub.Id,
                    Adress = item.Pub.Adress,
                    Name = item.Pub.Name
                }
            }).ToList();

            return mealsList;
        }
    }
}