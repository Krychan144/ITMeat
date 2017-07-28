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
using ITMeat.WEB.Models.Order;

namespace ITMeat.WEB.Hubs
{
    public class AppHub : Hub
    {
        private readonly IGetActivePubOrders _getActiveOrders;
        private readonly IGetPubMealByOrderId _getPubMealByOrderId;
        private readonly IGetActiveUserOrders _getUserOrders;
        private static readonly List<UserConnection> ConnectedClients = new List<UserConnection>();
        private const string TimeStampRepresentation = "dd-MM-yyyy HH:mm";

        public AppHub(IGetActivePubOrders getActiveOrders,
            IGetPubMealByOrderId pubMealByPubOrderId,
            IGetActiveUserOrders getUserOrders)
        {
            _getActiveOrders = getActiveOrders;
            _getPubMealByOrderId = pubMealByPubOrderId;
            _getUserOrders = getUserOrders;
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

        public void GetAddedMealsToJoinedPubOrder(Guid OrderId)
        {
            //ToDo create reposytory GetOrdersbyPubOrderId
            var OrderMealsList = _getPubMealByOrderId.Invoke(OrderId);

            var list = OrderMealsList.Select(item => new LoadPubOrderMealViewModel
            {
                PubId = item.Pub.Id,
                MealName = item.Name,
                Expense = 1,
                MealId = item.Id,
            });

            Clients.Caller.GetAddedMealsToJoinedPubOrder(list);
        }
    }
}