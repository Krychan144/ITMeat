using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.PubOrder.Interfaces
{
    public interface IDeletePubOrderByPubOrderId : IAction
    {
        bool Invoke(Guid pubOrderId);
    }
}