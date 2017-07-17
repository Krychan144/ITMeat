using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Implementations;

namespace ITMeat.BusinessLogic.Action.Order.Implementations
{
    public class GetActiveOrders : IGetActiveOrderscs
    {
        private readonly OrderRepository _orderRepository;

        public GetActiveOrders(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OrderModel> Invoke()
        {
            var dbOrders = _orderRepository.GetAll().ToList();

            if (dbOrders == null)
            {
                return null;
            }

            var orderList = dbOrders.Select(item => new OrderModel()
            {
                Id = item.Id,
                Name = item.Name,
            }).ToList();

            return orderList;
        }
    }
}