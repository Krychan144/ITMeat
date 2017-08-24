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

        public IQueryable<Meal> GetPubOferts(Guid pubId)
        {
            var query = from meal in context.Set<Meal>()
                        join mealtype in context.Set<MealType>() on meal.MealType.Id equals mealtype.Id
                        join pub in context.Set<Pub>() on meal.Pub.Id equals pub.Id
                        where pubId == pub.Id
                        select new Meal
                        {
                            Id = meal.Id,
                            Expense = meal.Expense,
                            MealType = new MealType
                            {
                                Id = mealtype.Id,
                                Name = mealtype.Name
                            },
                            Name = meal.Name,
                            Pub = new Pub
                            {
                                Id = pub.Id,
                                Name = pub.Name
                            }
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Meal>().AsQueryable() : query;
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
    }
}