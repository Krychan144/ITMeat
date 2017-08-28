using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ITMeat.BusinessLogic.Action.Adds.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Org.BouncyCastle.Security;

namespace ITMeat.BusinessLogic.Action.Adds.Implementations
{
    public class AddNewAdds : IAddNewAdds
    {
        private readonly IAddsRepository _addsRepository;
        private readonly IPubRepository _pubRepository;

        public AddNewAdds(IAddsRepository addsRepository,
            IPubRepository pubRepository)
        {
            _addsRepository = addsRepository;
            _pubRepository = pubRepository;
        }

        public Guid Invoke(AddsModel adds, Guid pubId)
        {
            if (!adds.IsValid() || pubId == Guid.Empty)
            {
                return Guid.Empty;
            }

            var pub = _pubRepository.GetById(pubId);

            if (pub == null)
            {
                return Guid.Empty;
            }

            var newAdds = AutoMapper.Mapper.Map<DataAccess.Models.Adds>(adds);
            newAdds.Pub = AutoMapper.Mapper.Map<DataAccess.Models.Pub>(pub);
            _addsRepository.Add(newAdds);
            _addsRepository.Save();

            return newAdds.Id;
        }
    }
}