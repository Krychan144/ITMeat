using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class AddNewMealType : IAddNewMealType
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public AddNewMealType(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public Guid Invoke(MealTypeModel mealType)
        {
            if (!mealType.IsValid())
            {
                return Guid.Empty;
            }

            var newMealType = AutoMapper.Mapper.Map<DataAccess.Models.MealType>(mealType);
            var ifexist = _mealTypeRepository.FindBy(c => c.Name == newMealType.Name);
            if (ifexist.Count() == 0)
            {
                _mealTypeRepository.Add(newMealType);
                _mealTypeRepository.Save();

                return newMealType.Id;
            }
            return ifexist.SingleOrDefault().Id;
        }
    }
}