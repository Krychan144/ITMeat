using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class GetMealTypeById : IGetMealTypeById
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetMealTypeById(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public MealTypeModel Invoke(Guid mealtypeId)
        {
            if (mealtypeId == Guid.Empty)
            {
                return null;
            }
            var dbMealType = _mealTypeRepository.GetById(mealtypeId);
            if (dbMealType == null)
            {
                return null;
            }
            var mealType = AutoMapper.Mapper.Map<MealTypeModel>(dbMealType);

            return mealType;
        }
    }
}