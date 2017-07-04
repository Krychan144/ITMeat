﻿using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.User.Interfaces
{
    public interface IGetUserByToken : IAction
    {
        UserModel Invoke(string token);
    }
}