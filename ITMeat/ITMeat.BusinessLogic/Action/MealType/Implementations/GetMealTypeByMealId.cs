using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class GetMealTypeByMealId : IGetMealTypeByMealId
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetMealTypeByMealId(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public List<MealTypeModel> Invoke(Guid mealId)
        {
            if (mealId == Guid.Empty)
            {
                return null;
            }
            var dbMealType = _mealTypeRepository.GetMealTypeByMealId(mealId);
            if (dbMealType == null)
            {
                return null;
            }

            var mealType = AutoMapper.Mapper.Map<List<MealTypeModel>>(dbMealType);

            return mealType;
        }
    }
}