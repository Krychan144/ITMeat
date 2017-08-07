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
        private readonly IOrderRepository _orderRepository;

        public AddMealsToUserOrders(IMealRepository mealRepository,
            IUserOrderMealRepository userOrderMealRepository,
            IUserOrderRepository orderRepository,
            IOrderRepository orderRepository1)
        {
            _mealRepository = mealRepository;
            _userOrderMealRepository = userOrderMealRepository;
            _userOrderRepository = orderRepository;
            _orderRepository = orderRepository1;
        }

        public Guid Invoke(Guid ordrId, Guid mealId, int quantity, UserOrderModel userOrderId)
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

            var dbOrder = _orderRepository.GetById(ordrId);
            if (dbOrder == null)
            {
                return Guid.Empty;
            }

            dbuserOrder.Expense = dbuserOrder.Expense + (quantity * dbMeal.Expense);
            dbOrder.Expense = dbOrder.Expense + (quantity * dbMeal.Expense);
            _userOrderMealRepository.Add(userOrderMeal);
            _userOrderMealRepository.Save();

            return userOrderMeal.Id;
        }
    }
}