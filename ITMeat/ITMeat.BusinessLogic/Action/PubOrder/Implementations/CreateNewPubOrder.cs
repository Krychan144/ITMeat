using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;
using System;
using ITMeat.DataAccess.Models;

namespace ITMeat.BusinessLogic.Action.PubOrder.Implementations
{
    public class CreateNewPubOrder : ICreateNewPubOrder
    {
        private readonly IPubOrderRepository _pubOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPubRepository _pubRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateNewPubOrder(IUserRepository userRepository,
            IPubOrderRepository pubOrderRepository,
            IPubRepository pubRepository,
            IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _pubOrderRepository = pubOrderRepository;
            _pubRepository = pubRepository;
            _orderRepository = orderRepository;
        }

        public Guid Invoke(Models.OrderModel order, Guid userId, Guid pubId)
        {
            if (!order.IsValid() || userId == Guid.Empty)
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

            var emptyOrder = AutoMapper.Mapper.Map<DataAccess.Models.Order>(order);
            emptyOrder.Owner = user;

            _orderRepository.Add(emptyOrder);

            var newPubOrder = new DataAccess.Models.PubOrder
            {
                Pub = pub,
                Order = emptyOrder
            };

            _pubOrderRepository.Add(newPubOrder);
            _pubOrderRepository.Save();

            return newPubOrder.Id;
        }
    }
}