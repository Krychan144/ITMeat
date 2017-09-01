using System;
using System.Linq;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Models.Aditionals;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IMealRepository : IGenericRepository<Meal>, IRepository
    {
        IQueryable<Meal> GetPubMeals(Guid pubId);

        IQueryable<Meal> GetPubMealByOrderId(Guid OrderId);

        Meal GetMealByUserOrderMealId(Guid userOrderMealId);

        Meal GetMealbyId(Guid mealId);

        IQueryable<Meal> GetMealInMealType(Guid mealTypeId);

        IQueryable<MostlySelectedMealInOrder> GetMealCountForAllUsers();
    }
}