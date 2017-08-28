using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class DeleteMealType : IDeleteMealType
    {
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly IMealRepository _mealRepository;

        public DeleteMealType(IMealTypeRepository mealTypeRepository,
            IMealRepository mealRepository)
        {
            _mealTypeRepository = mealTypeRepository;
            _mealRepository = mealRepository;
        }

        public bool Invoke(Guid mealTypeId)
        {
            if (mealTypeId == Guid.Empty)
            {
                return false;
            }
            var mealsTypeinMeals = _mealRepository.GetMealInMealType(mealTypeId);
            if (mealsTypeinMeals.Count() > 0)
            {
                return false;
            }
            var mealTypeToDelete = _mealTypeRepository.GetById(mealTypeId);
            if (mealTypeToDelete == null)
            {
                return false;
            }
            _mealTypeRepository.Delete(mealTypeToDelete);
            _mealTypeRepository.Save();
            return true;
        }
    }
}