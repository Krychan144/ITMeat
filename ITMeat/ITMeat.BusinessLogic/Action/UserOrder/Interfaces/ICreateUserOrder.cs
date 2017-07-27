using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.UserOrder.Interfaces
{
    public interface ICreateUserOrder : IAction
    {
        Guid Invoke(Guid userId, Guid pubOrderId);
    }
}