using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.User.Interfaces
{
    public interface IGetUserByEmail : IAction
    {
        UserModel Invoke(string email);
    }
}