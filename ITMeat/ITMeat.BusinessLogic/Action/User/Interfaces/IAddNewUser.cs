using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.User.Interfaces
{
    public interface IAddNewUser : IAction
    {
        DataAccess.Models.User Invoke(Models.UserModel user);
    }
}