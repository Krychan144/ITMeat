using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.BusinessLogic.Models.AditionalsModels;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Implementations
{
    public class GetSumExpenseByMealTypeAndUserId : IGetSumExpenseByMealTypeAndUserId
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public GetSumExpenseByMealTypeAndUserId(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public List<MealExpenseSumModel> Invoke(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return null;
            }
            var SumExpense = _mealTypeRepository.GetMealTypeSumeExpense(userId);
            if (SumExpense == null)
            {
                return null;
            }
            var sumExpenseToReturn = AutoMapper.Mapper.Map<List<MealExpenseSumModel>>(SumExpense);
            return sumExpenseToReturn;
        }
    }
}