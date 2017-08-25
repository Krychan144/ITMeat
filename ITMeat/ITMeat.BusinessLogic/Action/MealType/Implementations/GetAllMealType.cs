using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class GetAllMealType : IGetAllMealtype
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetAllMealType(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public List<MealTypeModel> Invoke()
        {
            var dbMealType = _mealTypeRepository.GetAll();
            if (dbMealType.Count() == 0)
            {
                return null;
            }
            var mealTypeList = AutoMapper.Mapper.Map<List<MealTypeModel>>(dbMealType);
            return mealTypeList;
        }
    }
}