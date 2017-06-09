using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementation
{
    internal class EmailRepository : GenericRepository<EmailMessage>, IEmailRepository
    {
        public EmailRepository(IITMeatDbContext context)
            : base(context)
        {
        }

        public override void Delete(EmailMessage entity)
        {
            context.Set<EmailMessage>().Remove(entity);
        }
    }
}