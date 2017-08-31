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
using ITMeat.WEB.Models.MealType;
using ITMeat.WEB.Models.Pub;
using ITMeat.WEB.Models.Pub.FormModels;
using ITMeat.WEB.Models.PubOrder;

namespace ITMeat.WEB.Controllers
{
    [Route("Order")]
    public class OrderController : BaseController
    {
        private readonly IGetAllPubs _getAllPubs;
        private readonly ICreateUserOrder _createUserOrder;
        private readonly IConvertDateTime _convertDateTime;
        private readonly IGetOrderById _getOrderById;
        private readonly IGetSumExpenseByMealTypeAndUserId _sumExpenseByMealTypeAndUserId;

        public OrderController(IGetAllPubs getAllPubs,
            ICreateUserOrder createUserOrder,
            IConvertDateTime convertDateTime,
            IGetOrderById getOrderById,
            IGetSumExpenseByMealTypeAndUserId sumExpenseByMealTypeAndUserId)
        {
            _getAllPubs = getAllPubs;
            _createUserOrder = createUserOrder;
            _convertDateTime = convertDateTime;
            _getOrderById = getOrderById;
            _sumExpenseByMealTypeAndUserId = sumExpenseByMealTypeAndUserId;
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
            var sumExpenseByMealType = _sumExpenseByMealTypeAndUserId.Invoke(ControllerContext.HttpContext.Actor());
            var model = sumExpenseByMealType.Select(item => new SumExpenseByMealType
            {
                MealTypeName = item.ItemName,
                SumExpense = item.Expense
            }).ToList();
            return View(model);
        }

        [HttpGet("SummaryOrderInHistory/{OrderId}")]
        public IActionResult SummaryOrderInHistory(Guid orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}