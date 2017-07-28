using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.UserOrder.Interfaces
{
    public interface IGetUserOrderByUserAndOrderId : IAction
    {
        UserOrderModel Invoke(Guid userId, Guid orderId);
    }
}