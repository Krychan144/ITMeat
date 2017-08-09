using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.PubOrder.Implementations
{
    public class GetUserSubmittedOrders : IGetUserSubmittedOrders
    {
        private readonly IPubOrderRepository _pubOrderRepository;

        public GetUserSubmittedOrders(IPubOrderRepository pubOrderRepository)
        {
            _pubOrderRepository = pubOrderRepository;
        }

        public List<PubOrderModel> Invoke(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return null;
            }

            var dbSubmittedOrders = _pubOrderRepository.GetUserSubmittedOrders(userId);
            if (dbSubmittedOrders == null)
            {
                return null;
            }

            var submittedOrders = AutoMapper.Mapper.Map<List<PubOrderModel>>(dbSubmittedOrders);

            return submittedOrders;
        }
    }
}