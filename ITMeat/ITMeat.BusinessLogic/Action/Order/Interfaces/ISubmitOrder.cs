﻿using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.Order.Interfaces
{
    public interface ISubmitOrder : IAction
    {
        bool Invoke(Guid orderId);
    }
}