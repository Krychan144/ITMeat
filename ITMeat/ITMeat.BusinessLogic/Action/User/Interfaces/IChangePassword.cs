using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.User.Interfaces
{
    public interface IChangePassword : IAction
    {
        bool Invoke(string email, string passwordOld, string newPassword, Guid userId);
    }
}