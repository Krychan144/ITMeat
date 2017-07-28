using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrder.Implementations
{
    public class GetUserOrderByUserAndOrderId : IGetUserOrderByUserAndOrderId
    {
        private readonly IUserOrderRepository _orderRepository;

        public GetUserOrderByUserAndOrderId(IUserOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public UserOrderModel Invoke(Guid userId, Guid orderId)
        {
            if (userId == Guid.Empty || userId == Guid.Empty)
            {
                return null;
            }

            var models = _orderRepository.GetUserOrderByUserAndOrderId(userId, orderId);

            if (models == null)
            {
                return null;
            }

            var UserOrderModel = AutoMapper.Mapper.Map<UserOrderModel>(models);

            return UserOrderModel;
        }
    }
}