using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;
using System;
using System.Linq;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Models;

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

        public Guid Invoke(Guid userId, Guid orderId)
        {
            if (orderId == Guid.Empty || userId == Guid.Empty)
            {
                return Guid.Empty;
            }

            var dbUserOrder = _userOrderRepository.GetUserOrders(orderId, userId);
            if (dbUserOrder.Any())
            {
                return Guid.Empty;
            }

            var dbuser = _userRepository.GetById(userId);
            if (dbuser == null)
            {
                return Guid.Empty;
            }

            var dborder = _orderRepository.GetById(orderId);
            if (dborder == null)
            {
                return Guid.Empty;
            }

            var newUserOrder = new DataAccess.Models.UserOrder
            {
                Order = dborder,
                User = dbuser,
                Expense = 0
            };

            _userOrderRepository.Add(newUserOrder);
            _userOrderRepository.Save();

            return newUserOrder.Id;
        }
    }
}