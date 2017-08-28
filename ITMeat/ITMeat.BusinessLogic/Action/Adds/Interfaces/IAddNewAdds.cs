using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Adds.Interfaces
{
    public interface IAddNewAdds : IAction
    {
        Guid Invoke(AddsModel adds, Guid pubId);
    }
}