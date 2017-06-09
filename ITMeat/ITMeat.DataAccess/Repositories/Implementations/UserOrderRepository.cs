using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementation
{
    public class UserOrderRepository : GenericRepository<UserOrder>, IUserOrderRepository
    {
        public UserOrderRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}