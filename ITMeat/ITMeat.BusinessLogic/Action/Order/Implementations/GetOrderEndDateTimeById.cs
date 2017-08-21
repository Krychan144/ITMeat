using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Order.Implementations
{
    public class GetOrderEndDateTimeById : IGetOrderEndDateTimeById
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderEndDateTimeById(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public DateTime? Invoke(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return null;
            }
            var dbOrder = _orderRepository.GetById(orderId);

            var dateEnd = dbOrder.EndDateTime;

            return dateEnd;
        }
    }
}