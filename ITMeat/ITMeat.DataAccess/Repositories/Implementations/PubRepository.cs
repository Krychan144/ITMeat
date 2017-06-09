using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementation
{
    public class PubRepository : GenericRepository<Pub>, IPubRepository
    {
        public PubRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}