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

        public IQueryable<Order> GetUserOrders(Guid userId)
        {
            var query = from order in context.Set<Order>()
                        join userOrder in context.Set<UserOrder>() on order.Id equals userOrder.Order.Id
                        join user in context.Set<User>() on order.Owner.Id equals user.Id
                        where user.Id == userId
                        where order.EndDateTime > DateTime.UtcNow
                        select new Order
                        {
                            Id = order.Id,
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Order>().AsQueryable() : query;
        }

        public IQueryable<Order> GetActiveUserOrders(Guid userid)
        {
            var query = from userOrder in context.Set<UserOrder>()
                        join order in context.Set<Order>() on userOrder.Order.Id equals order.Id
                        where order.EndDateTime > DateTime.UtcNow
                        where userOrder.User.Id == userid
                        select new Order
                        {
                            Id = order.Id,
                            Expense = order.Expense,
                            Owner = order.Owner,
                            SubmitDateTime = order.SubmitDateTime,
                            EndDateTime = order.EndDateTime,
                            CreatedOn = order.CreatedOn
                        };
            return !(query.Count() > 0) ? Enumerable.Empty<Order>().AsQueryable() : query;
        }
    }
}