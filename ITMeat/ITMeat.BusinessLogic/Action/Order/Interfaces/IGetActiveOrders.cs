using System;
using System.Collections.Generic;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Order.Interfaces
{
    public interface IGetActiveOrders : IAction
    {
        List<OrderModel> Invoke(Guid userId);
    }
}