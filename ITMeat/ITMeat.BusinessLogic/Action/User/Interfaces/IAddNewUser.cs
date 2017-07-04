using ITMeat.BusinessLogic.Action.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITMeat.BusinessLogic.Action.User.Interfaces
{
    public interface IAddNewUser : IAction
    {
        DataAccess.Models.User Invoke(Models.UserModel user);
    }
}