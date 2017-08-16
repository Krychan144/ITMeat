using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class MealTypeRepository : GenericRepository<MealType>, IMealTypeRepository
    {
        public MealTypeRepository(IITMeatDbContext context) : base(context)
        {
        }
    }
}