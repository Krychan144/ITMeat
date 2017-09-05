using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class UserOrderMealRepository : GenericRepository<UserOrderMeal>, IUserOrderMealRepository
    {
        public UserOrderMealRepository(IITMeatDbContext context)
            : base(context)
        {
        }

        public IQueryable<UserOrderMeal> GetOrderMeals(Guid orderId)
        {
            var query = from userOrderMeal in context.Set<UserOrderMeal>()
                        join userOrder in context.Set<UserOrder>() on userOrderMeal.UserOrder.Id equals userOrder.Id
                        join order in context.Set<Order>() on userOrder.Order.Id equals order.Id
                        where order.Id == orderId
                        where userOrderMeal.DeletedOn == null

                        select new UserOrderMeal
                        {
                            Id = userOrderMeal.Id,
                            CreatedOn = userOrderMeal.CreatedOn,
                            Quantity = userOrderMeal.Quantity,
                            Meal = userOrderMeal.Meal,
                            UserOrder = userOrderMeal.UserOrder,
                        };

            return !(query.Count() > 0) ? Enumerable.Empty<UserOrderMeal>().AsQueryable() : query;
        }
    }
}