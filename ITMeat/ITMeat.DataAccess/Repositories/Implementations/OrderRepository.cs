using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(IITMeatDbContext context) : base(context)
        {
        }

        public Order GetOrderByPubOrderId(Guid pubOrdrId)
        {
            var query = (from order in context.Set<Order>()
                         join pubOrder in context.Set<PubOrder>() on order.Id equals pubOrder.Order.Id
                         join user in context.Set<User>() on order.Owner.Id equals user.Id
                         where pubOrder.Id == pubOrdrId
                         where order.DeletedOn == null
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
                         }).SingleOrDefault();

            return query;
        }

        public IQueryable<UserOrderCountInPubs> GetUserOrderCountByPub(Guid userId)
        {
            var idQuery = context.Set<User>().Where(r => r.Id == userId)
                .Include(c => c.UserOrders).ThenInclude(t => t.Order).Include(d => d.UserOrders).ThenInclude(r => r.OrdersMeals).ThenInclude(r => r.Meal).ThenInclude(t => t.Pub)
                .Select(g => new UserOrderCountInPubs
                {
                    PubName = g.Key,
                    OrderCount = 4,
                });
            return !(idQuery.Count() > 0) ? Enumerable.Empty<UserOrderCountInPubs>().AsQueryable() : idQuery;
        }
    }
}