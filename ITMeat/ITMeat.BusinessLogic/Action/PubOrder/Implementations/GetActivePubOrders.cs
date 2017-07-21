using System.Collections.Generic;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.PubOrder.Implementations
{
    public class GetActivePubOrders : IGetActivePubOrders
    {
        private readonly IPubOrderRepository _pubOrderRepository;

        public GetActivePubOrders(IPubOrderRepository PuborderRepository)
        {
            _pubOrderRepository = PuborderRepository;
        }

        public List<PubOrderModel> Invoke()
        {
            var dbPubOrders = _pubOrderRepository.GetActiveOrders();

            if (dbPubOrders == null)
            {
                return null;
            }
            var orderList = AutoMapper.Mapper.Map<List<PubOrderModel>>(dbPubOrders);

            return orderList;
        }
    }
}