using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Implementations
{
    public class GetUserOrderMeals : IGetUserOrderMeals
    {
        private readonly IUserOrderMealRepository _userOrderMealRepository;

        public GetUserOrderMeals(IUserOrderMealRepository userOrderMealRepository)
        {
            _userOrderMealRepository = userOrderMealRepository;
        }

        public List<UserOrderMealModel> Invoke(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return null;
            }

            var dbUserOrderMealDB = _userOrderMealRepository.GetOrderMeals(orderId);

            if (dbUserOrderMealDB == null)
            {
                return null;
            }
            var mealList = dbUserOrderMealDB.Select(item => new UserOrderMealModel()
            {
                Id = item.Id,
                PubMeal = new MealModel
                {
                    Id = item.Meal.Id,
                    Name = item.Meal.Name,
                    Expense = item.Meal.Expense
                },
                UserOrder = new UserOrderModel
                {
                    Id = item.UserOrder.Id,
                    User = new UserModel
                    {
                        Id = item.UserOrder.User.Id,
                        Name = item.UserOrder.User.Name
                    },
                },
                Quantity = item.Quantity,
            }).ToList();

            return mealList;
        }
    }
}