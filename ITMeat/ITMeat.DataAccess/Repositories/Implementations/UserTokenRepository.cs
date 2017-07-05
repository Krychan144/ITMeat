using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using System;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(IITMeatDbContext context)
            : base(context)
        {
        }

        public override void Add(UserToken entity)
        {
            var timeNow = DateTime.UtcNow;
            entity.ModifiedOn = timeNow;
            entity.CreatedOn = timeNow;
            entity.SecretTokenTimeStamp = timeNow.AddHours(2);
            context.Set<UserToken>().Add(entity);
        }

        public override void Delete(UserToken entity)
        {
            context.Set<UserToken>().Remove(entity);
        }
    }
}