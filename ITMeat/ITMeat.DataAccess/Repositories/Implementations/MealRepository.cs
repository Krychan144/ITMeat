using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}