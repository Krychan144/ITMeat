using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.WEB.Models.Meal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITMeat.WEB.Controllers
{
    [Route("Meal")]
    public class MealController : BaseController
    {
        private readonly IGetMealTypeByPubId _getMealTypeByPubId;
        private readonly IGetMealById _getMealById;

        public MealController(IGetMealTypeByPubId getMealTypeByPubId,
            IGetMealById getMealById)
        {
            _getMealTypeByPubId = getMealTypeByPubId;
            _getMealById = getMealById;
        }

        [HttpGet("AddNewMealToPub/{PubId}")]
        public IActionResult AddNewMealToPub(Guid pubId)
        {
            var mealType = _getMealTypeByPubId.Invoke(pubId);
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

        [HttpGet("EditSelectedMeal/{MealId, PubId}")]
        public IActionResult EditSelectedMeal(Guid MealId, Guid PubId)
        {
            var meal = _getMealById.Invoke(MealId);
            //toDo
            var mealType = _getMealTypeByPubId.Invoke(PubId);
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

            return View();
        }

        [HttpGet("AddNewMealTypeToPub/{PubId}")]
        public IActionResult AddNewMealTypeToPub(Guid pubId)
        {
            return View();
        }
    }
}