using System;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.DataAccess.Repositories.Implementations;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Order.Implementations
{
    public class CreateNewOrder : ICreateNewOrder
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserOrderRepository _userOrderRepository;

        public CreateNewOrder(IOrderRepository orderRepository,
            IUserRepository userRepository,
            IUserOrderRepository userOrderRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _userOrderRepository = userOrderRepository;
        }

        public Guid Invoke(Models.OrderModel order, Guid userId)
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

            var newOrder = AutoMapper.Mapper.Map<DataAccess.Models.Order>(order);
            newOrder.Owner = AutoMapper.Mapper.Map<DataAccess.Models.User>(user);

            _orderRepository.Add(newOrder);
            _orderRepository.Save();

            var userOrder = new DataAccess.Models.UserOrder { User = user, Order = newOrder };

            _userOrderRepository.Add(userOrder);
            _userOrderRepository.Save();

            return newOrder.Id;
        }
    }
}