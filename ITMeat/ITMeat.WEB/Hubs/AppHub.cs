using System;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models;
using ITMeat.WEB.Models.PubOrder;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Implementations;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.WEB.Models.Meal.FormModels;
using ITMeat.WEB.Models.Order;
using ITMeat.WEB.Models.UserOrderMeals;
using Microsoft.AspNetCore.Mvc;

namespace ITMeat.WEB.Hubs
{
    public class AppHub : Hub
    {
        private readonly IGetActivePubOrders _getActiveOrders;
        private readonly IGetPubMealByOrderId _getPubMealByOrderId;
        private readonly IGetActiveUserOrders _getUserOrders;
        private readonly IGetUserOrderMeals _getUserOrderMeals;
        private readonly IAddNewMealsToUserOrders _addNewMealsToUserOrders;
        private readonly IDeleteUserOrderMeal _deleteUserOrderMeal;
        private readonly IGetUserOrderByUserAndOrderId _getUserOrderByUserAndOrderId;
        private readonly IGetUserOrderMealById _getUserOrderMealById;
        private readonly IGetMealByUserOrderMealId _getMealByUserOrderMealId;
        private static readonly List<UserConnection> ConnectedClients = new List<UserConnection>();
        private const string TimeStampRepresentation = "dd-MM-yyyy HH:mm";

        public AppHub(IGetActivePubOrders getActiveOrders,
            IGetPubMealByOrderId pubMealByPubOrderId,
            IGetActiveUserOrders getUserOrders,
            IGetUserOrderMeals getUserOrderMeals,
            IDeleteUserOrderMeal deleteUserOrderMeal,
            IGetUserOrderByUserAndOrderId getUserOrderByUserAndOrderId,
            IAddNewMealsToUserOrders addNewMealsToUserOrders,
            IGetUserOrderMealById getUserOrderMealById,
            IGetMealByUserOrderMealId getMealByUserOrderMealId)
        {
            _getActiveOrders = getActiveOrders;
            _getPubMealByOrderId = pubMealByPubOrderId;
            _getUserOrders = getUserOrders;
            _getUserOrderMeals = getUserOrderMeals;
            _deleteUserOrderMeal = deleteUserOrderMeal;
            _getUserOrderByUserAndOrderId = getUserOrderByUserAndOrderId;
            _addNewMealsToUserOrders = addNewMealsToUserOrders;
            _getUserOrderMealById = getUserOrderMealById;
            _getMealByUserOrderMealId = getMealByUserOrderMealId;
        }

        public override Task OnConnected()
        {
            ConnectedClients.Add(new UserConnection { ConnectionId = Context.ConnectionId, UserId = Context.Actor() });

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userToDelete = ConnectedClients.FirstOrDefault(x => x.UserId == Context.Actor());
            ConnectedClients.Remove(userToDelete);

            return base.OnDisconnected(stopCalled);
        }

        public void GetActivePubOrders()
        {
            var activeOrderList = _getActiveOrders.Invoke();
            var userOrderList = _getUserOrders.Invoke(Context.Actor());

            var list = activeOrderList.Select(item => new ActiveOrderViewModel
            {
                OwnerId = item.Order.Owner.Id,
                OwnerName = item.Order.Owner.Name,
                CreatedOn = item.Order.CreatedOn.ToLocalTime().ToString(TimeStampRepresentation),
                OrderId = item.Order.Id,
                EndDateTime = item.Order.EndDateTime.ToLocalTime().ToString(TimeStampRepresentation),
                PubId = item.Pub.Id,
                PubName = item.Pub.Name,
                PubOrderId = item.Id,
                IsJoined = userOrderList.Any(x => x.Id == item.Order.Id)
            });

            Clients.Caller.LoadActivePubOrders(list);
        }

        public void GetMealfromPub(Guid orderId)
        {
            var mealList = _getPubMealByOrderId.Invoke(orderId);

            var viewList = mealList.Select(item => new LoadPubOrderMealViewModel
            {
                PubId = item.Pub.Id,
                MealName = item.Name,
                Expense = item.Expense,
                MealId = item.Id,
            });

            Clients.Caller.PubMealLoadedAction(viewList);
        }

        public void GetUserOrders(Guid orderId)
        {
            var userOrderMeal = _getUserOrderMeals.Invoke(orderId);
            var viewList = userOrderMeal.Select(item => new GetUserOrderMealViewModel
            {
                UserName = item.UserOrder.User.Name,
                UserId = item.UserOrder.User.Id,
                Quantity = item.Quantity,
                Expense = item.Quantity * item.PubMeal.Expense,
                MealName = item.PubMeal.Name,
                Id = item.Id
            });

            Clients.Caller.GetUserOrderMeals(viewList);
        }

        public void DeleteUserOrderMeal(Guid userOrderMealId)
        {
            if (userOrderMealId == Guid.Empty)
            {
                return;
            }

            var userOrderMeal = _getPubMealByOrderId.Invoke(userOrderMealId);

            if (userOrderMeal == null)
            {
                return;
            }

            var deleted = _deleteUserOrderMeal.Invoke(userOrderMealId);

            if (deleted)
            {
                Clients.All.DeletedUserOrderMeal(userOrderMealId);
            }
        }

        public void AddNewMealToOrder(AddNewMealToOrderViewModel model, Guid orderId)
        {
            {
                var userOrderModel = _getUserOrderByUserAndOrderId.Invoke(Context.Actor(), orderId);
                if (userOrderModel == null)
                {
                    return;
                }

                var addMealAction = _addNewMealsToUserOrders.Invoke(orderId, model.MealId, model.Quantity, userOrderModel);

                if (addMealAction == Guid.Empty)
                {
                    return;
                }

                var addedMeal = _getUserOrderMealById.Invoke(addMealAction);
                if (addedMeal == null)
                {
                    return;
                }

                var meal = _getMealByUserOrderMealId.Invoke(addMealAction);

                var addedMealView = new GetUserOrderMealViewModel
                {
                    Id = addedMeal.Id,
                    Quantity = addedMeal.Quantity,
                    MealName = meal.Name,
                    Expense = addedMeal.Quantity * meal.Expense,
                    UserId = addedMeal.UserOrder.User.Id,
                    UserName = addedMeal.UserOrder.User.Name,
                };

                Clients.All.AddNewMealToUserOrder(addedMealView, orderId);
            }
        }
    }
}