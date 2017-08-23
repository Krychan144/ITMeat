using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class GetPubInfoById : IGetPubInfoById
    {
        private readonly IPubRepository _pubRepository;

        public GetPubInfoById(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public PubModel Invoke(Guid pubId)
        {
            if (pubId == Guid.Empty)
            {
                return null;
            }

            var dbPub = _pubRepository.GetById(pubId);
            if (dbPub == null)
            {
                return null;
            }
            var pub = AutoMapper.Mapper.Map<PubModel>(dbPub);

            return pub;
        }
    }
}