using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    internal class GetPubMealByOrderId : IGetPubMealByOrderId
    {
        private readonly IMealRepository _mealRepository;

        public GetPubMealByOrderId(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public List<MealModel> Invoke(Guid OrderId)
        {
            if (OrderId == Guid.Empty)
            {
                return null;
            }
            var dbMeals = _mealRepository.GetPubMealByOrderId(OrderId).ToList();

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
                },
                MealType = new MealTypeModel
                {
                    Id = item.MealType.Id,
                    Name = item.MealType.Name
                }
            }).ToList();

            return mealsList;
        }
    }
}