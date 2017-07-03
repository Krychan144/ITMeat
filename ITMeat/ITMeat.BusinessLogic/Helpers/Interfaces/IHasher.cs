using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Helpers.Interfaces
{
    public interface IHasher : IAction
    {
        string CreatePasswordHash(string password, string salt);

        string GenerateRandomSalt();

        string GenerateRandomGuid();
    }
}