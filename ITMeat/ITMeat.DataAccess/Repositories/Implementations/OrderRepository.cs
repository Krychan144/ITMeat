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

        public IQueryable<Order> GetOrderByPubOrderId(Guid pubOrdrId)
        {
            var query = from order in context.Set<Order>()
                        join pubOrder in context.Set<PubOrder>() on order.Id equals pubOrder.Order.Id
                        join user in context.Set<User>() on order.Owner.Id equals user.Id
                        where pubOrder.Id == pubOrdrId
                        select new Order
                        {
                            Id = order.Id,
                            Expense = order.Expense,
                            Owner = new User
                            {
                                Id = user.Id,
                            },
                            SubmitDateTime = order.SubmitDateTime,
                            EndDateTime = order.EndDateTime
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Order>().AsQueryable() : query;
        }
    }
}