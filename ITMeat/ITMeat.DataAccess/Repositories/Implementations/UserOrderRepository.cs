using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class UserOrderRepository : GenericRepository<UserOrder>, IUserOrderRepository
    {
        public UserOrderRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}