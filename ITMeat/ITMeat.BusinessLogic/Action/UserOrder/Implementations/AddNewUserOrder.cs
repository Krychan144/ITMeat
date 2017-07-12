using System;
using System.Linq;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.DataAccess.Repositories.Implementations;

namespace ITMeat.BusinessLogic.Action.UserOrder.Implementations
{
    public class AddNewUserOrder : IAddNewUserOrder
    {
        private readonly UserRepository _userRepository;
        private readonly OrderRepository _orderRepository;
        private readonly UserOrderRepository _userOrderRepository;

        public AddNewUserOrder(UserRepository userRepository,
            OrderRepository orderRepository,
            UserOrderRepository userOrderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _userOrderRepository = userOrderRepository;
        }

        public bool Invoke(Guid orderId, Guid userId)
        {
            if (orderId == Guid.Empty || userId == Guid.Empty || _userOrderRepository.FindBy(x => x.Order.Id == orderId && x.User.Id == userId).Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}