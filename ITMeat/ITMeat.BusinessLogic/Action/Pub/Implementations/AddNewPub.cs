using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class AddNewPub : IAddNewPub
    {
        private readonly IPubRepository _pubRepository;

        public AddNewPub(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public DataAccess.Models.Pub Invoke(Models.PubModel pub)
        {
            if (!pub.IsValid() || _pubRepository.FindBy(x => x.Id == pub.Id).Count() > 0)
            {
                return null;
            }

            var newPub = AutoMapper.Mapper.Map<DataAccess.Models.Pub>(pub);

            _pubRepository.Add(newPub);

            _pubRepository.Save();

            return newPub;
        }
    }
}