using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.User.Interfaces
{
    public interface IEditUserPassword : IAction
    {
        bool Invoke(Guid userId, string plaintextPassword);
    }
}