using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class GetPubInfoByOrderId : IGetPubInfoByOrderId
    {
        private readonly IPubRepository _pubRepository;

        public GetPubInfoByOrderId(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public PubModel Invoke(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return null;
            }

            var dbPub = _pubRepository.GetPubInfoByOrderId(orderId);
            if (dbPub == null)
            {
                return null;
            }

            var pubInfo = AutoMapper.Mapper.Map<PubModel>(dbPub);

            return pubInfo;
        }
    }
}