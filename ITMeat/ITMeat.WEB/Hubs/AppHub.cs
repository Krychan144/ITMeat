using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models.Order;
using Microsoft.AspNetCore.SignalR;

namespace ITMeat.WEB.Hubs
{
    public class AppHub : Hub
    {
        private readonly IGetActiveOrders _getActiveOrders;
        private static readonly List<UserConnection> ConnectedClients = new List<UserConnection>();
        private const string TimeStampRepresentation = "dd-MM-yyyy HH:mm";

        public AppHub(IGetActiveOrders getActiveOrders)
        {
            _getActiveOrders = getActiveOrders;
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

        public void GetActiveOrders()
        {
            var ActiveOrderList = _getActiveOrders.Invoke();

            var List = ActiveOrderList.Select(item => new ActiveOrderViewModel
            {
                Id = item.Id,
                PubId = item.PubId,
                CreatedOn = item.CreatedOn.ToLocalTime().ToString(TimeStampRepresentation),
                EndDateTime = item.EndDateTime.ToLocalTime().ToString(TimeStampRepresentation),
                PubName = item.Name,
                OwnerId = item.Owner.Id
            });

            Clients.Caller.LoadActiveOrders(List);
        }
    }
}