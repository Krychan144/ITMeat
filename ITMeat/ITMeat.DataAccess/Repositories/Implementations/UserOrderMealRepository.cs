using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class UserOrderMealRepository : GenericRepository<UserOrderMeal>, IUserOrderMealRepository
    {
        public UserOrderMealRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}