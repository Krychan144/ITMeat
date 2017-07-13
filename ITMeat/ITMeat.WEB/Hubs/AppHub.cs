using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models.Order;
using ITMeat.WEB.Models.User;
using Microsoft.AspNetCore.SignalR;

namespace ITMeat.WEB.Hubs
{
    public class AppHub : Hub
    {
        private readonly IAddNewUserOrder _addNewUserOrder;
        private readonly ICreateNewOrder _createNewOrder;

        private static readonly List<UserConnection> ConnectedClients = new List<UserConnection>();

        public AppHub(IAddNewUserOrder addNewUserOrder,
            ICreateNewOrder createNewOrder)
        {
            _addNewUserOrder = addNewUserOrder;
            _createNewOrder = createNewOrder;
        }

        public void CreateNewOrder(UserCreateNewOrderViewModel model)
        {
            if (string.IsNullOrEmpty(model.PubName))
            {
                return;
            }

            var roomModel = AutoMapper.Mapper.Map<OrderModel>(model);
            var room = _createNewOrder.Invoke(roomModel, Context.Actor());

            if (room != Guid.Empty)
            {
                Clients.Caller.orderAddedAction(model.PubName, model.EndOrders, model.StartOrders);
            }
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
    }
}