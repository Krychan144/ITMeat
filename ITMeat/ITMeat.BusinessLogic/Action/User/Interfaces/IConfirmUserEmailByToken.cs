using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Action.User.Interfaces
{
    public interface IConfirmUserEmailByToken : IAction
    {
        bool Invoke(string userId);
    }
}