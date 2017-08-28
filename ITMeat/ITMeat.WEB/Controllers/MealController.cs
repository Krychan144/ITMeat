using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Models.Common;
using ITMeat.WEB.Models.Meal;
using ITMeat.WEB.Models.Pub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITMeat.WEB.Controllers
{
    [Route("Meal")]
    public class MealController : BaseController
    {
        private readonly IGetMealTypeByPubId _getMealTypeByPubId;
        private readonly IGetMealById _getMealById;
        private readonly IGetPubByMealId _getPubByMealId;
        private readonly IEditMeal _editMeal;
        private readonly IGetAllMealtype _getAllMealtype;
        private readonly IGetPubOferts _getPubOferts;
        private readonly IDeleteMeal _deleteMeal;

        public MealController(IGetMealTypeByPubId getMealTypeByPubId,
            IGetMealById getMealById,
            IGetPubByMealId getPubByMealId,
            IEditMeal editMeal,
            IGetAllMealtype getAllMealtype, IGetPubOferts getPubOferts, IDeleteMeal deleteMeal)
        {
            _getMealTypeByPubId = getMealTypeByPubId;
            _getMealById = getMealById;
            _getPubByMealId = getPubByMealId;
            _editMeal = editMeal;
            _getAllMealtype = getAllMealtype;
            _getPubOferts = getPubOferts;
            _deleteMeal = deleteMeal;
        }

        [HttpGet("AddNewMealToPub/{PubId}")]
        public IActionResult AddNewMealToPub(Guid pubId)
        {
            var mealType = _getAllMealtype.Invoke();
            var model = new MealToAddViewModel() { MealTypes = new List<SelectListItem>() };

            foreach (var item in mealType)
            {
                model.MealTypes.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return View(model);
        }

        [HttpPost("AddNewMealToPub/{PubId}")]
        public IActionResult AddNewMealToPub(MealToAddViewModel model, Guid pubId)
        {
            Alert.Success("Success! Meal are added.");
            return RedirectToAction("PubOferts", "Pub");
        }

        [HttpGet("EditSelectedMeal/{MealId}")]
        public IActionResult EditSelectedMeal(Guid mealId)
        {
            var meal = _getMealById.Invoke(mealId);
            var pubid = _getPubByMealId.Invoke(mealId).Id;
            var mealType = _getMealTypeByPubId.Invoke(pubid);
            var model = new MealToEditViewModel()
            {
                MealTypes = new List<SelectListItem>(),
                MealId = meal.Id,
                MealName = meal.Name,
                MealTypeId = meal.MealType.Id,
                MealExpense = meal.Expense
            };

            foreach (var item in mealType)
            {
                model.MealTypes.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            return View(model);
        }

        [HttpPost("EditSelectedMeal/{MealId}")]
        public IActionResult EditSelectedMeal(MealToEditViewModel model)
        {
            var mealToeDIT = new MealModel
            {
                Id = model.MealId,
                Name = model.MealName,
                Expense = model.MealExpense,
                MealType = new MealTypeModel
                {
                    Id = model.MealTypeId
                }
            };
            var editMealAction = _editMeal.Invoke(mealToeDIT);
            var pub = _getPubByMealId.Invoke(model.MealId).Id;
            if (editMealAction == false)
            {
                Alert.Danger("Error! Meal are not edited.");
                return RedirectToAction("Meals", "Meal", new { PubId = pub });
            }
            Alert.Success("Success! Meal are edited.");
            return RedirectToAction("Meals", "Meal", new { PubId = pub });
        }

        [HttpGet("Meals/{PubId}")]
        public IActionResult Meals(Guid pubId)
        {
            var pubOferts = _getPubOferts.Invoke(pubId);
            var model = pubOferts.Select(item => new PubOfertsViewModel
            {
                MealId = item.Id,
                MealName = item.Name,
                MealExpense = item.Expense,
                MealTypeId = item.MealType.Id,
                MealTypeName = item.MealType.Name,
                PubId = item.Pub.Id,
                PubName = item.Pub.Name
            }).ToList();
            return View(model);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(DeleteItemViewModel model)
        {
            var pubId = _getPubByMealId.Invoke(model.id).Id;
            var deleteMealAction = _deleteMeal.Invoke(model.id);
            if (deleteMealAction == false)
            {
                Alert.Danger("Error! Meal are not deleted.");
                return RedirectToAction("Meals", "Meal", new { PubId = pubId });
            }
            Alert.Success("Success! Meal are deleted.");
            return RedirectToAction("Meals", "Meal", new { PubId = pubId });
        }
    }
}