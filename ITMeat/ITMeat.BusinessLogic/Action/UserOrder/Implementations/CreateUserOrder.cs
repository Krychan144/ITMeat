using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrder.Implementations
{
    public class CreateUserOrder : ICreateUserOrder
    {
        private readonly IUserOrderRepository _userOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPubOrderRepository _pubOrdersRepository;

        public CreateUserOrder(IUserOrderRepository userOrderRepository,
            IUserRepository userRepository,
            IOrderRepository orderRepository,
            IPubOrderRepository pubOrdersRepository1)
        {
            _userOrderRepository = userOrderRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _pubOrdersRepository = pubOrdersRepository1;
        }

        public Guid Invoke(Guid userId, Guid pubOrderId)
        {
            if (pubOrderId == Guid.Empty || userId == Guid.Empty || _pubOrdersRepository.FindBy(c => c.Order.Owner.Id == userId).Count() > 0)
            {
                return Guid.Empty;
            }

            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                return Guid.Empty;
            }

            var User = AutoMapper.Mapper.Map<DataAccess.Models.User>(user);

            var order = _orderRepository.GetOrderByPubOrderId(pubOrderId);

            if ((order == null))
            {
                return Guid.Empty;
            }

            var Order = AutoMapper.Mapper.Map<DataAccess.Models.Order>(order);

            var newUserOrder = new DataAccess.Models.UserOrder()
            {
                Order = Order,
                User = User,
            };

            _userOrderRepository.Add(newUserOrder);
            _userOrderRepository.Save();

            return newUserOrder.Id;
        }
    }
}