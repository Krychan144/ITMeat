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
            var query = (from pub in context.Set<Pub>()
                         join puborder in context.Set<PubOrder>() on pub.Id equals puborder.Pub.Id
                         join order in context.Set<Order>() on puborder.Order.Id equals order.Id
                         join user in context.Set<User>() on order.Owner.Id equals user.Id
                         where order.EndDateTime.AddHours(1) > DateTime.UtcNow && order.SubmitDateTime == null
                         where puborder.DeletedOn == null

                         select new PubOrder
                         {
                             Id = puborder.Id,
                             Order = new Order
                             {
                                 Owner = user,
                                 CreatedOn = order.CreatedOn,
                                 Id = order.Id,
                                 EndDateTime = order.EndDateTime,
                                 SubmitDateTime = order.SubmitDateTime,
                                 Expense = order.Expense,
                                 Name = order.Name
                             },
                             Pub = pub,
                         }).OrderBy(order => order.Order.EndDateTime);

            return !(query.Count() > 0) ? Enumerable.Empty<PubOrder>().AsQueryable() : query;
        }

        public IQueryable<PubOrder> GetUserSubmittedOrders(Guid userId)
        {
            var query = (from pub in context.Set<Pub>()
                         join puborder in context.Set<PubOrder>() on pub.Id equals puborder.Pub.Id
                         join order in context.Set<Order>() on puborder.Order.Id equals order.Id
                         join userOrder in context.Set<UserOrder>() on order.Id equals userOrder.Order.Id
                         join user in context.Set<User>() on userOrder.User.Id equals user.Id
                         where order.SubmitDateTime != null && puborder.DeletedOn == null
                         where userOrder.User.Id == userId || order.Owner.Id == userId

                         select new PubOrder
                         {
                             Id = puborder.Id,
                             Order = new Order
                             {
                                 Owner = user,
                                 CreatedOn = order.CreatedOn,
                                 Id = order.Id,
                                 EndDateTime = order.EndDateTime,
                                 SubmitDateTime = order.SubmitDateTime,
                                 Expense = order.Expense
                             },
                             Pub = pub,
                         }).OrderByDescending(order => order.Order.SubmitDateTime);

            return !(query.Count() > 0) ? Enumerable.Empty<PubOrder>().AsQueryable() : query;
        }
    }
}