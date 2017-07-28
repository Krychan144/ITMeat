using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Implementations
{
    public class AddMealsToUserOrders : IAddNewMealsToUserOrders
    {
        private readonly IMealRepository _mealRepository;
        private readonly IUserOrderMealRepository _userOrderMealRepository;
        private readonly IUserOrderRepository _userOrderRepository;

        public AddMealsToUserOrders(IMealRepository mealRepository,
            IUserOrderMealRepository userOrderMealRepository,
            IUserOrderRepository orderRepository)
        {
            _mealRepository = mealRepository;
            _userOrderMealRepository = userOrderMealRepository;
            _userOrderRepository = orderRepository;
        }

        public Guid Invoke(Guid mealId, int quantity, UserOrderModel userOrderId)
        {
            if (userOrderId == null || mealId == Guid.Empty || quantity == 0)
            {
                return Guid.Empty;
            }

            var dbMeal = _mealRepository.GetById(mealId);
            if (dbMeal == null)
            {
                return Guid.Empty;
            }
            var dbuserOrder = _userOrderRepository.GetById(userOrderId.Id);

            if (dbuserOrder == null)
            {
                return Guid.Empty;
            }
            var userOrderMeal = new UserOrderMeal
            {
                Meal = dbMeal,
                UserOrder = dbuserOrder,
                Quantity = quantity
            };

            _userOrderMealRepository.Add(userOrderMeal);
            _userOrderMealRepository.Save();

            return userOrderMeal.Id;
        }
    }
}