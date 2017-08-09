using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.PubOrder.Implementations
{
    public class GetPubOrderByPubOrderId : IGetPubOrderByPubOrderId
    {
        private readonly IPubOrderRepository _pubOrderRepository;

        public GetPubOrderByPubOrderId(IPubOrderRepository pubOrderRepository)
        {
            _pubOrderRepository = pubOrderRepository;
        }

        public PubOrderModel Invoke(Guid pubOrderId)
        {
            if (pubOrderId == Guid.Empty)
            {
                return null;
            }

            var pubOrderDb = _pubOrderRepository.GetById(pubOrderId);
            if (pubOrderDb == null)
            {
                return null;
            }

            var pubOrder = AutoMapper.Mapper.Map<PubOrderModel>(pubOrderDb);

            return pubOrder;
        }
    }
}