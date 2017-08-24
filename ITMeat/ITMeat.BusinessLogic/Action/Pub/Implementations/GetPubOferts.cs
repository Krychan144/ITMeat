using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class GetPubOferts : IGetPubOferts
    {
        private readonly IPubRepository _pubRepository;

        public GetPubOferts(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public List<MealModel> Invoke(Guid pubId)
        {
            if (pubId == Guid.Empty)
            {
                return null;
            }
            var dbOferts = _pubRepository.GetPubOferts(pubId);
            if (dbOferts == null)
            {
                return null;
            }
            var Oferts = AutoMapper.Mapper.Map<List<MealModel>>(dbOferts);

            return Oferts;
        }
    }
}