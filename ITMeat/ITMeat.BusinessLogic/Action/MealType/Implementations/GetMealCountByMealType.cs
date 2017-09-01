using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.BusinessLogic.Models.AditionalsModels;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class GetMealCountByMealType : IGetMealCountByMealType
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetMealCountByMealType(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public List<MealCountForAllUsersModel> Invoke()
        {
            var mealList = _mealTypeRepository.GetMealCountByTypeForAllUsers();
            if (mealList == null)
            {
                return null;
            }
            var returnList = AutoMapper.Mapper.Map<List<MealCountForAllUsersModel>>(mealList);
            return returnList;
        }
    }
}