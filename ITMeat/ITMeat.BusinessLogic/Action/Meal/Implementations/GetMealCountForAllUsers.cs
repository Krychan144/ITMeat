using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Models.AditionalsModels;
using ITMeat.DataAccess.Models.Aditionals;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class GetMealCountForAllUsers : IGetMealCountForAllUsers
    {
        private readonly IMealRepository _mealRepository;

        public GetMealCountForAllUsers(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public List<MostlySelectedMealInOrderModel> Invoke()
        {
            var dbList = _mealRepository.GetMealCountForAllUsers();
            if (dbList == null)
            {
                return null;
            }

            var returnList = AutoMapper.Mapper.Map<List<MostlySelectedMealInOrderModel>>(dbList);
            return returnList;
        }
    }
}