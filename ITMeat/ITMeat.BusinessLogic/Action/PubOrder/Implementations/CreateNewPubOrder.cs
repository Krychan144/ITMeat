using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;
using System;

namespace ITMeat.BusinessLogic.Action.PubOrder.Implementations
{
    public class CreateNewPubOrder : ICreateNewPubOrder
    {
        private readonly IPubOrderRepository _pubOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPubRepository _pubRepository;

        public CreateNewPubOrder(IUserRepository userRepository,
            IPubOrderRepository pubOrderRepository,
            IPubRepository pubRepository)
        {
            _userRepository = userRepository;
            _pubOrderRepository = pubOrderRepository;
            _pubRepository = pubRepository;
        }

        public Guid Invoke(Models.PubOrderModel puborder, Guid userId, Guid pubId)
        {
            if (!puborder.IsValid() || userId == Guid.Empty)
            {
                return Guid.Empty;
            }

            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                return Guid.Empty;
            }

            var pub = _pubRepository.GetById(pubId);

            if ((pub == null))
            {
                return Guid.Empty;
            }
            {
            }
            var newOrder = AutoMapper.Mapper.Map<DataAccess.Models.PubOrder>(puborder);
            newOrder.Owner = user;
            newOrder.Pub = pub;

            _pubOrderRepository.Add(newOrder);
            _pubOrderRepository.Save();

            return newOrder.Id;
        }
    }
}