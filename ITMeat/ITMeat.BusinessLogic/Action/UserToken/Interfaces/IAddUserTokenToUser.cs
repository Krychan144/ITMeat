using System;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.UserToken.Interfaces
{
    public interface IAddUserTokenToUser : IAction
    {
        string Invoke(Guid userId);
    }
}