using System;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ITMeat.BusinessLogic.Action.UserToken.Implementations;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class GetAllPubs : IGetAllPubs
    {
        private readonly IPubRepository _pubRepository;

        public GetAllPubs(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public List<PubModel> Invoke()
        {
            var dbPubs = _pubRepository.GetAll().ToList();

            if (dbPubs == null)
            {
                return null;
            }

            var pubList = dbPubs.Select(item => new PubModel()
            {
                Id = item.Id,
                Name = item.Name,
                Adress = item.Adress,
            }).ToList();

            return pubList;
        }
    }
}