using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.MealType.Implementations
{
    public class EditMealType : IEditMealType
    {
        private readonly IMealTypeRepository _mealTypeRepository;

        public EditMealType(IMealTypeRepository mealTypeRepository)
        {
            _mealTypeRepository = mealTypeRepository;
        }

        public bool Invoke(MealTypeModel model)
        {
            if (model == null)
            {
                return false;
            }

            var mealTypeToEdit = _mealTypeRepository.GetById(model.Id);

            if (mealTypeToEdit == null)
            {
                return false;
            }
            var ifexist = _mealTypeRepository.FindBy(c => c.Name == model.Name);
            if (ifexist.Count() == 0)
            {
                mealTypeToEdit.Name = model.Name;

                _mealTypeRepository.Edit(mealTypeToEdit);
                _mealTypeRepository.Save();
                return true;
            }
            return false;
        }
    }
}