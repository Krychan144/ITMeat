using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Models.MealType;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITMeat.WEB.Controllers
{
    [Route("MealType")]
    public class MealTypeController : BaseController
    {
        private readonly IGetAllMealtype _getAllMealtype;
        private readonly IAddNewMealType _addNewMeal;
        private readonly IGetMealTypeById _getMealTypeById;
        private readonly IEditMealType _editMealType;

        public MealTypeController(IGetAllMealtype getAllMealtype,
            IAddNewMealType addNewMeal,
            IGetMealTypeById getMealTypeById,
            IEditMealType editMealType)
        {
            _getAllMealtype = getAllMealtype;
            _addNewMeal = addNewMeal;
            _getMealTypeById = getMealTypeById;
            _editMealType = editMealType;
        }

        [HttpGet("MealType")]
        public IActionResult MealType()
        {
            var mealTypeList = _getAllMealtype.Invoke();
            var model = mealTypeList.Select(item => new AllMealType
            {
                MealTypeId = item.Id,
                MealTypeName = item.Name,
            }).ToList();
            return View(model);
        }

        [HttpGet("AddNewMealType")]
        public IActionResult AddNewMealType()
        {
            return View();
        }

        [HttpPost("AddNewMealType")]
        public IActionResult AddNewMealType(MealTypeToAdd model)
        {
            var mealTypeToAddModel = new MealTypeModel
            {
                Name = model.MealTypeName,
            };
            var addMealTypeAction = _addNewMeal.Invoke(mealTypeToAddModel);

            if (addMealTypeAction == Guid.Empty)
            {
                Alert.Success("Meal Type are added.");
                return RedirectToAction("MealType", "MealType");
            };
            Alert.Danger("Error. Meal Type not added.");
            return RedirectToAction("MealType", "MealType");
        }

        [HttpGet("EditMealType/{mealTypeId}")]
        public IActionResult EditMealType(Guid mealTypeId)
        {
            var mealTyp = _getMealTypeById.Invoke(mealTypeId);
            var model = new MealTypeToEditViewModel
            {
                MealTypeId = mealTyp.Id,
                MealtypeName = mealTyp.Name
            };
            return View(model);
        }

        [HttpPost("EditMealType/{mealTypeId}")]
        public IActionResult EditMealType(MealTypeToEditViewModel model)
        {
            var mealTypeToEdit = new MealTypeModel
            {
                Id = model.MealTypeId,
                Name = model.MealtypeName
            };
            var editMealType = _editMealType.Invoke(mealTypeToEdit);
            if (editMealType == true)
            {
                Alert.Success("Success! Meal Type are edited.");
                return RedirectToAction("MealType", "MealType");
            }
            Alert.Danger("Error. Meal Type about this name are exist! ");
            return RedirectToAction("MealType", "MealType");
        }
    }
}