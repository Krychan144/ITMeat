using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace ITMeat.BusinessLogic.Action.UserOrder.Implementations
{
    public class GetActiveOrders : IGetActiveOrders
    {
        private readonly IOrderRepository _orderRepository;

        public GetActiveOrders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OrderModel> Invoke(Guid userId)
        {
            var dbOrders = _orderRepository.GetActiveUserOrders(userId);

            if (dbOrders == null)
            {
                return null;
            }
            var DbOrderlist = AutoMapper.Mapper.Map<List<OrderModel>>(dbOrders);

            return DbOrderlist;
        }
    }
}