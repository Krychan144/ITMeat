using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

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
    }
}