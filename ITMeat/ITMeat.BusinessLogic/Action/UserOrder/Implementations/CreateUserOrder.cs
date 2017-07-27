using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;
using System;
using System.Linq;

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
            if (pubOrderId == Guid.Empty || userId == Guid.Empty || _userOrderRepository.GetUserOrders(pubOrderId, userId).Count() > 0)
            {
                return Guid.Empty;
            }

            var dbuser = _userRepository.GetById(userId);

            if (dbuser == null)
            {
                return Guid.Empty;
            }

            var user = AutoMapper.Mapper.Map<DataAccess.Models.User>(dbuser);

            var dborder = _orderRepository.GetOrderByPubOrderId(pubOrderId);

            if (dborder == null)
            {
                return Guid.Empty;
            }

            var order = AutoMapper.Mapper.Map<DataAccess.Models.Order>(dborder.FirstOrDefault());

            var newUserOrder = new DataAccess.Models.UserOrder
            {
                Order = order,
                User = user,
                Expense = 0
            };

            _userOrderRepository.Add(newUserOrder);
            _userOrderRepository.Save();

            return newUserOrder.Id;
        }
    }
}