using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IUserOrderMealRepository : IGenericRepository<UserOrderMeal>, IRepository
    {
        IQueryable<UserOrderMeal> GetOrderMeals(Guid orderId);

        IQueryable<Meal> GetAllUserOrderMeals(Guid userId);
    }
}