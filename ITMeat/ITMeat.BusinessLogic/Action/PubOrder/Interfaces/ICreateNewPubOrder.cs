using System;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.PubOrder.Interfaces
{
    public interface ICreateNewPubOrder : IAction
    {
        Guid Invoke(DateTime endDateTime, Guid userId, Guid pubId);
    }
}