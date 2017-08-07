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

        public IQueryable<PubOrder> GetActiveOrders()
        {
            var query = from pub in context.Set<Pub>()
                        join puborder in context.Set<PubOrder>() on pub.Id equals puborder.Pub.Id
                        join order in context.Set<Order>() on puborder.Order.Id equals order.Id
                        join user in context.Set<User>() on order.Owner.Id equals user.Id
                        where order.EndDateTime > DateTime.UtcNow

                        select new PubOrder
                        {
                            Id = puborder.Id,
                            Order = new Order
                            {
                                Owner = user,
                                CreatedOn = order.CreatedOn,
                                Id = order.Id,
                                EndDateTime = order.EndDateTime
                            },
                            Pub = pub,
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<PubOrder>().AsQueryable() : query;
        }
    }
}