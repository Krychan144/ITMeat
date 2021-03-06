﻿using System;
using System.Linq;
using System.Linq.Expressions;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Models.Aditionals;
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

        public IQueryable<Meal> GetMealInMealType(Guid mealTypeId)
        {
            var query = from meal in context.Set<Meal>()
                        join mealtype in context.Set<MealType>() on meal.MealType.Id equals mealtype.Id
                        where mealTypeId == mealtype.Id
                        where meal.DeletedOn == null
                        select new Meal
                        {
                            Id = meal.Id,
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
                        where meal.DeletedOn == null
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
                         where meal.DeletedOn == null
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
                         where meal.DeletedOn == null
                         select new Meal
                         {
                             Id = meal.Id,
                             Expense = meal.Expense,
                             MealType = mealType,
                             Name = meal.Name
                         }).FirstOrDefault();

            return query;
        }

        public IQueryable<MostlySelectedMealInOrder> GetMealCountForAllUsers()
        {
            var idQuery = context.Set<UserOrderMeal>()
                .Where(r => r.UserOrder.Order.SubmitDateTime != null)
                .GroupBy(e => e.Meal.Name)
                .Select(g => new MostlySelectedMealInOrder
                {
                    MealName = g.Key,
                    CountValue = g.Sum(e => e.Quantity)
                })
                .OrderByDescending(g => g.CountValue)
                .Take(6);
            return !(idQuery.Count() > 0) ? Enumerable.Empty<MostlySelectedMealInOrder>().AsQueryable() : idQuery;
        }

        public IQueryable<Meal> GetPubMeals(Guid pubId)
        {
            var query = from meal in context.Set<Meal>()
                        join mealtype in context.Set<MealType>() on meal.MealType.Id equals mealtype.Id
                        join pub in context.Set<Pub>() on meal.Pub.Id equals pub.Id
                        where pubId == pub.Id
                        where meal.DeletedOn == null
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

        public IQueryable<Meal> GetAllUserOrderMeals(Guid userId)
        {
            var query = from userOrderMeal in context.Set<UserOrderMeal>()
                        join userOrder in context.Set<UserOrder>() on userOrderMeal.UserOrder.Id equals userOrder.Id
                        join user in context.Set<User>() on userOrder.User.Id equals user.Id
                        join meal in context.Set<Meal>() on userOrderMeal.Meal.Id equals meal.Id
                        join mealType in context.Set<MealType>() on meal.MealType.Id equals mealType.Id
                        where userOrderMeal.DeletedOn == null
                        where userOrder.DeletedOn == null
                        where user.Id == userId

                        select new Meal
                        {
                            Id = meal.Id,
                            Expense = meal.Expense,
                            MealType = new MealType()
                            {
                                Id = mealType.Id,
                                Name = mealType.Name
                            },
                            Name = meal.Name
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<Meal>().AsQueryable() : query;
        }
    }
}