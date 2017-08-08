using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Implementations
{
    public class DeleteUserOrderMeal : IDeleteUserOrderMeal
    {
        private readonly IUserOrderMealRepository _userOrderMealRepository;

        public DeleteUserOrderMeal(IUserOrderMealRepository userOrderMealRepository)
        {
            _userOrderMealRepository = userOrderMealRepository;
        }

        public bool Invoke(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var mealToDelete = _userOrderMealRepository.GetById(id);
            if (mealToDelete == null)
            {
                return false;
            }

            _userOrderMealRepository.Delete(mealToDelete);
            _userOrderMealRepository.Save();
            return true;
        }
    }
}