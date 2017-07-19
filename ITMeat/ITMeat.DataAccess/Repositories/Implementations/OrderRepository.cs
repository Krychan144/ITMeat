using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(IITMeatDbContext context) : base(context)
        {
        }

        public IQueryable<Order> GetOrdersActive()
        {
            var query = from order in context.Set<Order>()
                        join pub in context.Set<Pub>() on order.PubId equals pub.Id
                        where order.EndDateTime > DateTime.UtcNow
                        select new Order
                        {
                            Id = order.Id,
                            Name = pub.Name,
                            EndDateTime = order.EndDateTime,
                            CreatedOn = order.CreatedOn,
                            Owner = order.Owner,
                            Meals = order.Meals,
                            Expense = order.Expense,
                            PubId = pub.Id
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Order>().AsQueryable() : query;
        }
    }
}