using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.WEB.Models.Meal.FormModels;
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

        private const string TimeStampRepresentation = "dd-MM-yyyy HH:mm:SS";

        public OrderController(IGetAllPubs getAllPubs,
            ICreateNewPubOrder createNewPubOrder,
            ICreateUserOrder createUserOrder,
            IGetOrderEndDateTimeById getOrderEndDateTimeById,
            IConvertDateTime convertDateTime)
        {
            _getAllPubs = getAllPubs;
            _createNewPubOrder = createNewPubOrder;
            _createUserOrder = createUserOrder;
            _getOrderEndDateTimeById = getOrderEndDateTimeById;
            _convertDateTime = convertDateTime;
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
            var dateEnd = _getOrderEndDateTimeById.Invoke(orderId).ToLocalTime();
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
    }
}