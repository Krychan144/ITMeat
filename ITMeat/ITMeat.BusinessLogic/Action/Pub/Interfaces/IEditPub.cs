﻿using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Pub.Interfaces
{
    public interface IEditPub : IAction
    {
        bool Invoke(PubModel model);
    }
}