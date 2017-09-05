using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class PubRepository : GenericRepository<Pub>, IPubRepository
    {
        public PubRepository(IITMeatDbContext context)
            : base(context)
        {
        }

        public Pub GetPubByOrderId(Guid orderId)
        {
            var query = (from pub in context.Set<Pub>()
                         join pubOrder in context.Set<PubOrder>() on pub.Id equals pubOrder.Pub.Id
                         join order in context.Set<Order>() on pubOrder.Order.Id equals order.Id
                         where orderId == order.Id
                         select new Pub
                         {
                             Id = pub.Id,
                             Adress = pub.Adress,
                             Name = pub.Name,
                             Phone = pub.Phone,
                             FreeDelivery = pub.FreeDelivery
                         }).FirstOrDefault();

            return query;
        }

        public Pub GetPubInfoByOrderId(Guid orderId)
        {
            var query = (from pub in context.Set<Pub>()
                         join pubOrder in context.Set<PubOrder>() on pub.Id equals pubOrder.Pub.Id
                         join order in context.Set<Order>() on pubOrder.Order.Id equals order.Id
                         where orderId == order.Id
                         select new Pub
                         {
                             Id = pub.Id,
                             Adress = pub.Adress,
                             Name = pub.Name,
                             Phone = pub.Phone
                         }).FirstOrDefault();

            return query;
        }

        public Pub GetPubByMealId(Guid mealId)
        {
            var query = (from meal in context.Set<Meal>()
                         join pub in context.Set<Pub>() on meal.Pub.Id equals pub.Id
                         where mealId == meal.Id
                         where meal.DeletedOn == null
                         select new Pub
                         {
                             Id = pub.Id,
                             Name = pub.Name
                         }).FirstOrDefault();

            return query;
        }
    }
}