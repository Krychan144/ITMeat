using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Implementations;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Order.Implementations
{
    public class GetActiveOrders : IGetActiveOrders
    {
        private readonly IOrderRepository _orderRepository;

        public GetActiveOrders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OrderModel> Invoke()
        {
            var dbOrders = _orderRepository.GetOrdersActive();

            if (dbOrders == null)
            {
                return null;
            }

            var orderList = dbOrders.ToList().Select(item => new OrderModel()
            {
                Id = item.Id,
                Name = item.Name,
            }).ToList();

            return orderList;
        }
    }
}