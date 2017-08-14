using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;
using Org.BouncyCastle.Asn1;

namespace ITMeat.BusinessLogic.Action.Order.Implementations
{
    public class SubmitOrder : ISubmitOrder
    {
        private readonly IOrderRepository _orderRepository;

        public SubmitOrder(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool Invoke(Guid orderid)
        {
            if (orderid == Guid.Empty)
            {
                return false;
            }

            var dbOrder = _orderRepository.GetById(orderid);
            if (dbOrder == null)
            {
                return false;
            }
            dbOrder.SubmitDateTime = DateTime.UtcNow;

            _orderRepository.Edit(dbOrder);
            _orderRepository.Save();
            return true;
        }
    }
}