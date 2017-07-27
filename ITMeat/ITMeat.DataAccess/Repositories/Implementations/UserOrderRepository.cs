using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class UserOrderRepository : GenericRepository<UserOrder>, IUserOrderRepository
    {
        public UserOrderRepository(IITMeatDbContext context)
            : base(context)
        {
        }

        public IQueryable<Order> GetActiveUserOrders(Guid userid)
        {
            var query = from userOrder in context.Set<UserOrder>()
                            // where order.EndDateTime > DateTime.UtcNow
                        where userOrder.User.Id == userid
                        select new Order
                        {
                            Id = userOrder.Order.Id,
                            Expense = userOrder.Order.Expense,
                            Owner = userOrder.Order.Owner,
                            SubmitDateTime = userOrder.Order.SubmitDateTime,
                            EndDateTime = userOrder.Order.EndDateTime,
                            CreatedOn = userOrder.Order.CreatedOn
                        };
            return !(query.Count() > 0) ? Enumerable.Empty<Order>().AsQueryable() : query;
        }

        public IQueryable<UserOrder> GetUserOrders(Guid pubOrderId, Guid userid)
        {
            var query = from user in context.Set<User>()
                        join userorder in context.Set<UserOrder>() on user.Id equals userorder.User.Id
                        join order in context.Set<Order>() on userorder.Order.Id equals order.Id
                        join pubOrder in context.Set<PubOrder>() on order.Id equals pubOrder.Order.Id
                        where pubOrder.Id == pubOrderId
                        where user.Id == userid
                        select new UserOrder
                        {
                            Id = userorder.Id,
                        };
            return !(query.Count() > 0) ? Enumerable.Empty<UserOrder>().AsQueryable() : query;
        }
    }
}