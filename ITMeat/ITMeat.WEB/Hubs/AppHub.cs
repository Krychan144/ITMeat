using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models;
using ITMeat.WEB.Models.PubOrder;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Hubs
{
    public class AppHub : Hub
    {
        private readonly IGetActivePubOrders _getActiveOrders;
        private static readonly List<UserConnection> ConnectedClients = new List<UserConnection>();
        private const string TimeStampRepresentation = "dd-MM-yyyy HH:mm";

        public AppHub(IGetActivePubOrders getActiveOrders)
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
                OwnerId = item.Owner.Id,
                OwnerName = item.Owner.Name,
                CreatedOn = item.CreatedOn.ToLocalTime().ToString(TimeStampRepresentation),
                Id = item.Id,
                EndDateTime = item.EndDateTime.ToLocalTime().ToString(TimeStampRepresentation),
                PubId = item.Pub.Id,
                PubName = item.Pub.Name
            });

            Clients.Caller.LoadActiveOrders(List);
        }
    }
}