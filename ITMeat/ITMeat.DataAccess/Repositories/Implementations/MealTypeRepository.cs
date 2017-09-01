using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Models.Aditionals;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Clauses;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class MealTypeRepository : GenericRepository<MealType>, IMealTypeRepository
    {
        public MealTypeRepository(IITMeatDbContext context) : base(context)
        {
        }

        public IQueryable<MealType> GetMealTypes(Guid pubId)
        {
            var query = from mealType in context.Set<MealType>()
                        join meal in context.Set<Meal>() on mealType.Id equals meal.MealType.Id
                        join pub in context.Set<Pub>() on meal.Pub.Id equals pub.Id
                        where pub.Id == pubId
                        where mealType.DeletedOn == null
                        group mealType by mealType.Name
                into g
                        select g.FirstOrDefault();
            return !(query.Count() > 0) ? Enumerable.Empty<MealType>().AsQueryable() : query;
        }

        public IQueryable<MealType> GetMealTypeByMealId(Guid mealId)
        {
            var query = (from pub in context.Set<Pub>()
                         join meal in context.Set<Meal>() on pub.Id equals meal.Pub.Id
                         join mealType in context.Set<MealType>() on meal.MealType.Id equals mealType.Id
                         where meal.Id == mealId
                         where mealType.DeletedOn == null
                         select new MealType
                         {
                             Id = mealType.Id,
                             Name = mealType.Name
                         });
            return !(query.Count() > 0) ? Enumerable.Empty<MealType>().AsQueryable() : query;
        }

        public IQueryable<MealType> GetMealTypesInUserOrder(Guid userId)
        {
            var query = from userOrderMeal in context.Set<UserOrderMeal>()
                        join userOrder in context.Set<UserOrder>() on userOrderMeal.UserOrder.Id equals userOrder.Id
                        join user in context.Set<User>() on userOrder.User.Id equals user.Id
                        join meal in context.Set<Meal>() on userOrderMeal.Meal.Id equals meal.Id
                        join mealType in context.Set<MealType>() on meal.MealType.Id equals mealType.Id
                        where userId == user.Id
                        where mealType.DeletedOn == null
                        group mealType by mealType.Name
                into g
                        select g.FirstOrDefault();
            return !(query.Count() > 0) ? Enumerable.Empty<MealType>().AsQueryable() : query;
        }

        public IQueryable<MealExpenseSum> GetMealTypeSumeExpense(Guid userId)
        {
            var idQuery = context.Set<UserOrderMeal>()
                .Where(uo => uo.UserOrder.User.Id == userId)
                .Include(m => m.Meal)
                .ThenInclude(mt => mt.MealType)
                .GroupBy(e => e.Meal.MealType.Name)
                .Select(g => new MealExpenseSum
                {
                    ItemName = g.Key,
                    Expense = g.Sum(d => d.Meal.Expense * d.Quantity),
                });
            return !(idQuery.Count() > 0) ? Enumerable.Empty<MealExpenseSum>().AsQueryable() : idQuery;
        }

        public IQueryable<MealCountForAllUsers> GetMealCountByTypeForAllUsers()
        {
            var idQuery = context.Set<UserOrderMeal>()
                .GroupBy(e => e.Meal.MealType.Name)
                .Select(g => new MealCountForAllUsers
                {
                    MealTypeName = g.Key,
                    CountValue = g.Sum(e => e.Quantity)
                });
            return !(idQuery.Count() > 0) ? Enumerable.Empty<MealCountForAllUsers>().AsQueryable() : idQuery;
        }
    }
}