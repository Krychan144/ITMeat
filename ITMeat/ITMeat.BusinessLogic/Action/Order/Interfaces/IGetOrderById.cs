﻿using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Order.Interfaces
{
    public interface IGetOrderById : IAction
    {
        OrderModel Invoke(Guid orderId);
    }
}