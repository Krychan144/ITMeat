using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class GetPubByOrderId : IGetPubByOrderId
    {
        private readonly IPubRepository _pubRepository;

        public GetPubByOrderId(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public PubModel Invoke(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return null;
            }
            var dbPub = _pubRepository.GetPubByOrderId(orderId);
            if (dbPub == null)
            {
                return null;
            }
            var pub = AutoMapper.Mapper.Map<PubModel>(dbPub);
            return pub;
        }
    }
}