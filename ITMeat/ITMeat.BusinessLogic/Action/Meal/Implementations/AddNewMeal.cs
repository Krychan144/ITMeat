using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using System;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class AddNewMeal : IAddNewMeal
    {
        private readonly IMealRepository _mealRepository;
        private readonly IPubRepository _pubRepository;
        private readonly IMealTypeRepository _mealTypeRepository;

        public AddNewMeal(IMealRepository mealRepository, IPubRepository pubRepository, IMealTypeRepository mealTypeRepository)
        {
            _mealRepository = mealRepository;
            _pubRepository = pubRepository;
            _mealTypeRepository = mealTypeRepository;
        }

        public Guid Invoke(MealModel meal, Guid pubId, Guid mealTypeId)
        {
            if (!meal.IsValid() || pubId == Guid.Empty || mealTypeId == Guid.Empty)
            {
                return Guid.Empty;
            }

            var pub = _pubRepository.GetById(pubId);

            if (pub == null)
            {
                return Guid.Empty;
            }

            var mealType = _mealTypeRepository.GetById(mealTypeId);

            if (mealType == null)
            {
                return Guid.Empty;
            }

            var newMeal = AutoMapper.Mapper.Map<DataAccess.Models.Meal>(meal);
            newMeal.Pub = AutoMapper.Mapper.Map<DataAccess.Models.Pub>(pub);
            newMeal.MealType = AutoMapper.Mapper.Map<DataAccess.Models.MealType>(mealType);

            _mealRepository.Add(newMeal);
            _mealRepository.Save();

            return newMeal.Id;
        }
    }
}