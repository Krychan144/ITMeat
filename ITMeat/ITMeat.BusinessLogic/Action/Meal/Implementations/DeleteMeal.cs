using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Base;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class DeleteMeal : IDeleteMeal
    {
        private readonly IMealRepository _mealRepository;

        public DeleteMeal(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public bool Invoke(Guid mealId)
        {
            if (mealId == Guid.Empty)
            {
                return false;
            }
            var mealToDelete = _mealRepository.GetById(mealId);
            if (mealToDelete == null)
            {
                return false;
            }
            _mealRepository.Delete(mealToDelete);
            _mealRepository.Save();
            return true;
        }
    }
}