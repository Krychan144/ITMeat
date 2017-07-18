using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Configuration.Interfaces
{
    public interface IDbContextSeeder : IAction
    {
        void Seed();
    }
}