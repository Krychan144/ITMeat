using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IITMeatDbContext context)
            : base(context)
        {
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
    }
}