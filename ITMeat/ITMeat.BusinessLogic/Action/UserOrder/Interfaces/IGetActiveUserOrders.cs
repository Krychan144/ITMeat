using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Models;

namespace ITMeat.BusinessLogic.Action.UserOrder.Interfaces
{
    public interface IGetActiveUserOrders : IAction
    {
        List<OrderModel> Invoke(Guid userId);
    }
}