using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.Pub.Interfaces
{
    public interface IAddNewPub : IAction
    {
        DataAccess.Models.Pub Invoke(Models.PubModel pub);
    }
}