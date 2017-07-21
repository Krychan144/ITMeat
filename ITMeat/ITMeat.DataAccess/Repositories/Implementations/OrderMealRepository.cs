using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class OrderMealRepository : GenericRepository<OrderMeal>, IOrderMealRepository
    {
        public OrderMealRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}