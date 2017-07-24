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

        public AddNewMeal(IMealRepository mealRepository, IPubRepository pubRepository)
        {
            _mealRepository = mealRepository;
            _pubRepository = pubRepository;
        }

        public Guid Invoke(MealModel meal, Guid pubId)
        {
            if (!meal.IsValid() || pubId == Guid.Empty)
            {
                return Guid.Empty;
            }

            var pub = _pubRepository.GetById(pubId);

            if (pub == null)
            {
                return Guid.Empty;
            }

            var newMeal = AutoMapper.Mapper.Map<DataAccess.Models.Meal>(meal);
            newMeal.Pub = AutoMapper.Mapper.Map<DataAccess.Models.Pub>(pub);

            _mealRepository.Add(newMeal);
            _mealRepository.Save();

            return newMeal.Id;
        }
    }
}