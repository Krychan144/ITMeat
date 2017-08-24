using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.WEB.Models.Meal;
using ITMeat.WEB.Models.Meal.FormModels;
using ITMeat.WEB.Models.Pub;
using ITMeat.WEB.Models.Pub.FormModels;
using ITMeat.WEB.Models.PubOrder;

namespace ITMeat.WEB.Controllers
{
    [Route("Order")]
    public class OrderController : BaseController
    {
        private readonly IGetAllPubs _getAllPubs;
        private readonly ICreateNewPubOrder _createNewPubOrder;
        private readonly ICreateUserOrder _createUserOrder;
        private readonly IGetOrderEndDateTimeById _getOrderEndDateTimeById;
        private readonly IConvertDateTime _convertDateTime;
        private readonly IGetPubInfoById _getPubInfoById;
        private readonly IGetOrderById _getOrderById;
        private readonly IAddNewPub _addNewPub;
        private readonly IEditPub _editPub;
        private readonly IGetPubOferts _getPubOferts;
        private readonly IGetMealTypeByPubId _getMealTypeByPubId;

        public OrderController(IGetAllPubs getAllPubs,
            ICreateNewPubOrder createNewPubOrder,
            ICreateUserOrder createUserOrder,
            IGetOrderEndDateTimeById getOrderEndDateTimeById,
            IConvertDateTime convertDateTime,
            IGetOrderById getOrderById,
            IAddNewPub addNewPub,
            IGetPubInfoById getPubInfoById,
            IEditPub editPub,
            IGetPubOferts getPubOferts,
            IGetMealTypeByPubId getMealTypeByPubId)
        {
            _getAllPubs = getAllPubs;
            _createNewPubOrder = createNewPubOrder;
            _createUserOrder = createUserOrder;
            _getOrderEndDateTimeById = getOrderEndDateTimeById;
            _convertDateTime = convertDateTime;
            _getOrderById = getOrderById;
            _addNewPub = addNewPub;
            _getPubInfoById = getPubInfoById;
            _editPub = editPub;
            _getPubOferts = getPubOferts;
            _getMealTypeByPubId = getMealTypeByPubId;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("NewOrder")]
        public IActionResult NewOrder()
        {
            var pubList = _getAllPubs.Invoke();
            var model = new AddNewOrderViewModel { Pubs = new List<SelectListItem>() };

            foreach (var item in pubList)
            {
                model.Pubs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            return View(model);
        }

        [HttpGet("ActiveOrders")]
        public IActionResult ActiveOrders()
        {
            return View();
        }

        [HttpGet("SubmitOrder/{OrderId}")]
        public IActionResult SubmitOrder(Guid orderId)
        {
            ViewBag.OrderId = orderId;

            var dateEnd = _getOrderById.Invoke(orderId).EndDateTime.ToLocalTime();
            ViewBag.OrderEndDateTime = _convertDateTime.MilliTimeStamp(dateEnd);
            return View();
        }

        [HttpGet("JoinToPubOrders/{OrderId}")]
        public IActionResult JoinToPubOrders(Guid orderId)
        {
            ViewBag.OrderId = orderId;

            var AddUserOrderAction = _createUserOrder.Invoke(ControllerContext.HttpContext.Actor(), orderId);

            if (AddUserOrderAction == Guid.Empty)
            {
                Alert.Success("Complete you order.");
                return RedirectToAction("SubmitOrder", "Order");
            }
            Alert.Success("Great, Now you can add yours Meals.");
            return RedirectToAction("SubmitOrder", "Order");
        }

        [HttpGet("OrdersHistory")]
        public IActionResult OrdersHistory()
        {
            return View();
        }

        [HttpGet("SummaryOrder/{OrderId}")]
        public IActionResult SummaryOrder(Guid orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        [HttpGet("Statistic")]
        public IActionResult Statistic()
        {
            return View();
        }

        [HttpGet("SummaryOrderInHistory/{OrderId}")]
        public IActionResult SummaryOrderInHistory(Guid orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        [HttpGet("AddNewPub")]
        public IActionResult AddNewPub()
        {
            return View();
        }

        [HttpPost("AddNewPub")]
        public IActionResult AddNewPub(AddNewPubViewModel model)
        {
            var pubToAddModel = new PubModel
            {
                Adress = model.Adress,
                Name = model.Name,
                Phone = model.Phone,
                FreeDelivery = model.FreeDelivery
            };
            var addPubAction = _addNewPub.Invoke(pubToAddModel);

            if (addPubAction != null)
            {
                Alert.Success("Pub are added.");
                return RedirectToAction("Index", "Order");
            };
            Alert.Danger("Error. Pub are not added.");
            return RedirectToAction("AddNewPub", "Order");
        }

        [HttpGet("SelectPubToEdit")]
        public IActionResult SelectPubToEdit()
        {
            var pubList = _getAllPubs.Invoke();
            var model = new SelectPubToEditViewModel { Pubs = new List<SelectListItem>() };

            foreach (var item in pubList)
            {
                model.Pubs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            return View(model);
        }

        [HttpPost("SelectPubToEdit")]
        public IActionResult SelectPubToEdit(SelectPubToEditViewModel model)
        {
            return RedirectToAction("EditPubInformations", "Order", new { model.PubId });
        }

        [HttpGet("EditPubInformations/{PubId}")]
        public IActionResult EditPubInformations(Guid pubId)
        {
            var pubView = _getPubInfoById.Invoke(pubId);
            var model = new PubInfoViewModel
            {
                PubId = pubView.Id,
                FreeDelivery = pubView.FreeDelivery,
                Phone = pubView.Phone,
                Name = pubView.Name,
                Address = pubView.Adress
            };
            return View(model);
        }

        [HttpPost("EditPubInformations/{PubId}")]
        public IActionResult EditPubInformations(PubInfoViewModel model)
        {
            var pubToEdit = new PubModel
            {
                Id = model.PubId,
                Name = model.Name,
                Phone = model.Phone,
                FreeDelivery = model.FreeDelivery,
                Adress = model.Address
            };
            var EditPub = _editPub.Invoke(pubToEdit);
            if (EditPub == true)
            {
                Alert.Success("Success! Pub are edited.");
                return RedirectToAction("EditPubInformations", "Order", new { model.PubId });
            }
            Alert.Danger("Error. Pub are not edited");
            return RedirectToAction("EditPubInformations", "Order", new { model.PubId });
        }

        [HttpGet("PubOferts/{PubId}")]
        public IActionResult PubOferts(Guid pubId)
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
            return RedirectToAction("PubOferts", "Order");
        }

        [HttpGet("EditSelectedMeal")]
        public IActionResult EditSelectedMeal()
        {
            return View();
        }

        [HttpGet("AddNewMealTypeToPub/{PubId}")]
        public IActionResult AddNewMealTypeToPub(Guid pubId)
        {
            return View();
        }
    }
}