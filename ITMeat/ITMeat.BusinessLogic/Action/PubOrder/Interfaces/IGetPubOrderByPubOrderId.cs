using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.PubOrder.Interfaces
{
    public interface IGetPubOrderByPubOrderId : IAction
    {
        PubOrderModel Invoke(Guid pubOrderId);
    }
}