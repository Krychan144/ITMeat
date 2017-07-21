using System;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.PubOrder.Interfaces
{
    public interface ICreateNewPubOrder : IAction
    {
        Guid Invoke(Models.PubOrderModel order, Guid userId, Guid pubId);
    }
}