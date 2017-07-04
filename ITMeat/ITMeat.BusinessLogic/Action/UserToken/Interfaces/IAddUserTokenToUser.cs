using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.UserToken.Implementations
{
    public interface IAddUserTokenToUser : IAction
    {
        string Invoke(Guid userId);
    }
}