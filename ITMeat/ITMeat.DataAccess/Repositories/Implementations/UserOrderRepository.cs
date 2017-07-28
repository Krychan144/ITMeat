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

        public IQueryable<UserOrder> GetUserOrders(Guid orderId, Guid userid)
        {
            var query = from user in context.Set<User>()
                        join userorder in context.Set<UserOrder>() on user.Id equals userorder.User.Id
                        join order in context.Set<Order>() on userorder.Order.Id equals order.Id
                        where order.Id == orderId
                        where user.Id == userid
                        select new UserOrder
                        {
                            Id = userorder.Id,
                        };
            return !(query.Count() > 0) ? Enumerable.Empty<UserOrder>().AsQueryable() : query;
        }

        public UserOrder GetUserOrderByUserAndOrderId(Guid userId, Guid orderId)
        {
            var query = (from order in context.Set<Order>()
                         join userOrder in context.Set<UserOrder>() on order.Id equals userOrder.Order.Id
                         join user in context.Set<User>() on userOrder.User.Id equals user.Id
                         where user.Id == userId
                         where order.Id == orderId
                         select new UserOrder
                         {
                             Id = userOrder.Id,
                             User = userOrder.User,
                             Expense = userOrder.Expense,
                             Order = userOrder.Order
                         }).SingleOrDefault();
            return query;
        }
    }
}