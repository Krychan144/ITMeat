using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Helpers.Interfaces
{
    public interface IConvertDateTime : IAction
    {
        double MilliTimeStamp(DateTime TheDate);
    }
}