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
        private static readonly List<UserConnection> ConnectedClients = new List<UserConnection>();

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