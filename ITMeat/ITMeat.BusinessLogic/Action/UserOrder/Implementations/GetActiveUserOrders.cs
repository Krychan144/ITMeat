using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrder.Implementations
{
    public class GetActiveUserOrders : IGetActiveUserOrders
    {
        private readonly IUserOrderRepository _userOrderRepository;

        public GetActiveUserOrders(IUserOrderRepository userOrderRepository)
        {
            _userOrderRepository = userOrderRepository;
        }

        public List<OrderModel> Invoke(Guid userId)
        {
            var dbOrders = _userOrderRepository.GetActiveUserOrders(userId);

            if (dbOrders == null)
            {
                return null;
            }
            var DbOrderlist = AutoMapper.Mapper.Map<List<OrderModel>>(dbOrders);

            return DbOrderlist;
        }
    }
}