using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.BusinessLogic.Action.Email.Interfaces
{
    public interface IAddNewEmailMessage : IAction
    {
        bool Invoke(EmailMessageModel email);
    }
}