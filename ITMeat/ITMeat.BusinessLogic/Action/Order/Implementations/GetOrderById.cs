using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Order.Implementations
{
    public class GetOrderById : IGetOrderById
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderById(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderModel Invoke(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var dbOrder = _orderRepository.GetById(id);
            if (dbOrder == null)
            {
                return null;
            }

            var order = AutoMapper.Mapper.Map<OrderModel>(dbOrder);

            return order;
        }
    }
}