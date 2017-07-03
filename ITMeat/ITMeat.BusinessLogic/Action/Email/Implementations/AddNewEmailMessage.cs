using ITMeat.BusinessLogic.Action.Email.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Email.Implementations
{
    public class AddNewEmailMessage : IAddNewEmailMessage
    {
        private readonly IEmailRepository emailRepository;

        public AddNewEmailMessage(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public bool Invoke(EmailMessageModel email)
        {
            if (!email.IsValid())
            {
                return false;
            }

            var dbMessage = AutoMapper.Mapper.Map<EmailMessage>(email);

            emailRepository.Add(dbMessage);
            emailRepository.Save();

            return true;
        }
    }
}