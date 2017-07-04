using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.UserToken.Interfaces
{
    public interface IGetUserTokenByUserId : IAction
    {
        UserTokenModel Invoke(Guid userId);
    }
}