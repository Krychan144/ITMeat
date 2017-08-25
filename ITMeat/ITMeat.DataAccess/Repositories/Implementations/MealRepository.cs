using System;
using System.Linq;
using System.Linq.Expressions;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(IITMeatDbContext context)
            : base(context)
        {
        }

        public IQueryable<Meal> GetPubMeals(Guid pubId)
        {
            var query = from meal in context.Set<Meal>()
                        join pub in context.Set<Pub>() on meal.Pub.Id equals pub.Id
                        where meal.Pub.Id == pubId
                        select new Meal
                        {
                            Id = meal.Id,
                            Pub = new Pub
                            {
                                Id = pub.Id,
                                Adress = pub.Adress,
                                FreeDelivery = pub.FreeDelivery
                            },
                            Expense = meal.Expense,
                            Name = meal.Name,
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Meal>().AsQueryable() : query;
        }

        public IQueryable<Meal> GetPubMealByOrderId(Guid orderId)
        {
            var query = from meal in context.Set<Meal>()
                        join pub in context.Set<Pub>() on meal.Pub.Id equals pub.Id
                        join pubOrder in context.Set<PubOrder>() on pub.Id equals pubOrder.Pub.Id
                        join order in context.Set<Order>() on pubOrder.Order.Id equals order.Id
                        join mealType in context.Set<MealType>() on meal.MealType.Id equals mealType.Id
                        where order.Id == orderId
                        select new Meal
                        {
                            Id = meal.Id,
                            Pub = pub,
                            Expense = meal.Expense,
                            Name = meal.Name,
                            MealType = mealType
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Meal>().AsQueryable() : query;
        }

        public Meal GetMealByUserOrderMealId(Guid userOrderMealId)
        {
            var query = (from meal in context.Set<Meal>()
                         join userOrderMeal in context.Set<UserOrderMeal>() on meal.Id equals userOrderMeal.Meal.Id
                         where userOrderMealId == userOrderMeal.Id
                         select new Meal
                         {
                             Id = meal.Id,
                             Name = meal.Name,
                             Expense = meal.Expense,
                         }).SingleOrDefault();

            return query;
        }

        public Meal GetMealbyId(Guid mealId)
        {
            var query = (from meal in context.Set<Meal>()
                         join mealType in context.Set<MealType>() on meal.MealType.Id equals mealType.Id
                         where meal.Id == mealId
                         select new Meal
                         {
                             Id = meal.Id,
                             Expense = meal.Expense,
                             MealType = mealType,
                             Name = meal.Name
                         }).FirstOrDefault();

            return query;
        }
    }
}