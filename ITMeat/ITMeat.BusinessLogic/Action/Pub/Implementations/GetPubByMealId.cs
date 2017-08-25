using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class GetPubByMealId : IGetPubByMealId
    {
        private readonly IPubRepository _pubRepository;

        public GetPubByMealId(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public PubModel Invoke(Guid mealId)
        {
            if (mealId == Guid.Empty)
            {
                return null;
            }

            var dbPub = _pubRepository.GetPubByMealId(mealId);
            if (dbPub == null)
            {
                return null;
            }
            var pub = AutoMapper.Mapper.Map<PubModel>(dbPub);

            return pub;
        }
    }
}