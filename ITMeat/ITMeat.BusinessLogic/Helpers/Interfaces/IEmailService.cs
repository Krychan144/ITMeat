using ITMeat.BusinessLogic.Action.Base;
using MimeKit;
using System.Threading.Tasks;

namespace ITMeat.BusinessLogic.Helpers.Interfaces
{
    public interface IEmailService : IAction
    {
        Task<bool> ConnectClient();

        Task<bool> DisconnectClient();

        Task<bool> SendEmailAsync(MimeMessage emailMessage);

        MimeMessage CreateMessage(string emailAddress, string subject, string message);
    }
}