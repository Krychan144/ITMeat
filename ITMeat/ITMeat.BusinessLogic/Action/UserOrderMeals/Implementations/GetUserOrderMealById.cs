using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserOrderMeals.Implementations
{
    public class GetUserOrderMealById : IGetUserOrderMealById
    {
        private readonly IUserOrderMealRepository _userOrderMealRepository;

        public GetUserOrderMealById(IUserOrderMealRepository userOrderMealRepository)
        {
            _userOrderMealRepository = userOrderMealRepository;
        }

        public UserOrderMealModel Invoke(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var dbUserOrderMeal = _userOrderMealRepository.GetById(id);

            if (dbUserOrderMeal == null)
            {
                return null;
            }

            var userOrderMeal = AutoMapper.Mapper.Map<UserOrderMealModel>(dbUserOrderMeal);

            return userOrderMeal;
        }
    }
}