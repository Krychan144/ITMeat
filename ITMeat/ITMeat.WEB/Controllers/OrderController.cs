using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.WEB.Models.Meal.FormModels;
using ITMeat.WEB.Models.PubOrder;

namespace ITMeat.WEB.Controllers
{
    [Route("Order")]
    public class OrderController : BaseController
    {
        private readonly IGetAllPubs _getAllPubs;
        private readonly ICreateNewPubOrder _createNewPubOrder;
        private readonly IGetActivePubOrders _getActiveOrders;
        private readonly ICreateUserOrder _createUserOrder;

        public OrderController(IGetAllPubs getAllPubs,
            IGetActivePubOrders getActiveOrders,
            ICreateNewPubOrder createNewPubOrder, ICreateUserOrder createUserOrder)
        {
            _getAllPubs = getAllPubs;
            _getActiveOrders = getActiveOrders;
            _createNewPubOrder = createNewPubOrder;
            _createUserOrder = createUserOrder;
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

        [HttpPost("NewOrder")]
        public IActionResult NewOrder(AddNewOrderViewModel model)
        {
            var Order = new OrderModel
            {
                CreatedOn = DateTime.UtcNow,
                EndDateTime = model.EndOrders,
            };
            var orderAddAction = _createNewPubOrder.Invoke(Order, ControllerContext.HttpContext.Actor(), model.PubId);

            if (orderAddAction == Guid.Empty)
            {
                Alert.Danger("Zamowienie już istnieje");
                return View();
            }
            Alert.Success("Great, Order are create.");

            return RedirectToAction("Index", "Order");
        }

        [HttpGet("ActiveOrders")]
        public IActionResult ActiveOrders()
        {
            return View();
        }

        [HttpGet("SubmitOrder/{PubOrderId}")]
        public IActionResult SubmitOrder(Guid PubOrderId)
        {
            ViewBag.PubOrderId = PubOrderId;
            return View();
        }

        [HttpGet("JoinToPubOrders/{PubOrderId}")]
        public IActionResult JoinToPubOrders(Guid PubOrderId)
        {
            ViewBag.PubOrderId = PubOrderId;

            var AddUserOrderAction = _createUserOrder.Invoke(ControllerContext.HttpContext.Actor(), PubOrderId);

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

        [HttpPost("SubmitOrder/{PubOrderId}")]
        public IActionResult SubmitOrder(Guid PubOrderId, AddNewMealToOrderViewModel model)
        {
            return RedirectToAction("ActiveOrders", "Order");
        }
    }
}