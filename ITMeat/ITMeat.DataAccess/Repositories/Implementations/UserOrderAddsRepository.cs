using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class UserOrderAddsRepository : GenericRepository<UserOrderAdds>, IUserOrderAddsRepository
    {
        public UserOrderAddsRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}