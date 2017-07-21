using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class PubOrderRepository : GenericRepository<PubOrder>, IPubOrderRepository
    {
        public PubOrderRepository(IITMeatDbContext context) : base(context)
        {
        }

        public IQueryable<PubOrder> GetActivePubOrders()
        {
            var query = from puborder in context.Set<PubOrder>()
                        where puborder.EndDateTime > DateTime.UtcNow
                        select new PubOrder
                        {
                            CreatedOn = puborder.CreatedOn,
                            EndDateTime = puborder.EndDateTime,
                            Id = puborder.Id,
                            Pub = puborder.Pub,
                            Owner = puborder.Owner
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<PubOrder>().AsQueryable() : query;
        }
    }
}