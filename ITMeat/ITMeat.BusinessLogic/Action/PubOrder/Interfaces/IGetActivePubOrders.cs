using System.Collections.Generic;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.PubOrder.Interfaces
{
    public interface IGetActivePubOrders : IAction
    {
        List<PubOrderModel> Invoke();
    }
}