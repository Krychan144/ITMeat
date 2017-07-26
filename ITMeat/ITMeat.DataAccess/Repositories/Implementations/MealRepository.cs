using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

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
                            Pub = pub,
                            Expense = meal.Expense,
                            Name = meal.Name,
                            Type = meal.Type,
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Meal>().AsQueryable() : query;
        }

        public IQueryable<Meal> GetPubMealByPubOrderId(Guid pubOrderId)
        {
            var query = from meal in context.Set<Meal>()
                        join pub in context.Set<Pub>() on meal.Pub.Id equals pub.Id
                        join pubOrder in context.Set<PubOrder>() on pub.Id equals pubOrder.Pub.Id
                        where pubOrder.Id == pubOrderId
                        select new Meal
                        {
                            Id = meal.Id,
                            Pub = pub,
                            Expense = meal.Expense,
                            Name = meal.Name,
                            Type = meal.Type,
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Meal>().AsQueryable() : query;
        }
    }
}