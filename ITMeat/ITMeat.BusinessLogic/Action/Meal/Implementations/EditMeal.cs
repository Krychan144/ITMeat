using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Meal.Implementations
{
    public class EditMeal : IEditMeal
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMealTypeRepository _mealTypeRepository;

        public EditMeal(IMealRepository mealRepository, IMealTypeRepository mealTypeRepository)
        {
            _mealRepository = mealRepository;
            _mealTypeRepository = mealTypeRepository;
        }

        public bool Invoke(MealModel model)
        {
            if (model == null)
            {
                return false;
            }

            var dbMealtype = _mealTypeRepository.GetById(model.MealType.Id);
            if (dbMealtype == null)
            {
                return false;
            }
            var mealToEdit = _mealRepository.GetById(model.Id);

            if (mealToEdit == null)
            {
                return false;
            }

            mealToEdit.Name = model.Name;
            mealToEdit.Expense = model.Expense;
            mealToEdit.MealType = dbMealtype;
            _mealRepository.Edit(mealToEdit);
            _mealRepository.Save();
            return true;
        }
    }
}