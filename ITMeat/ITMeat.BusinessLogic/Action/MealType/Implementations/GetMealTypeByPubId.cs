using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class GetMealTypeByPubId : IGetMealTypeByPubId
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetMealTypeByPubId(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public List<MealTypeModel> Invoke(Guid pubId)
        {
            if (pubId == Guid.Empty)
            {
                return null;
            }

            var dbList = _mealTypeRepository.GetMealTypes(pubId);
            if (dbList == null)
            {
                return null;
            }

            var mealTypeList = AutoMapper.Mapper.Map<List<MealTypeModel>>(dbList);
            return mealTypeList;
        }
    }
}