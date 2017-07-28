using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
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
        private readonly IGetUserOrderByUserAndOrderId _getUserOrderByUserAndOrderId;
        private readonly IAddNewMealsToUserOrders _addNewMealsToUserOrders;

        public OrderController(IGetAllPubs getAllPubs,
            ICreateNewPubOrder createNewPubOrder,
            ICreateUserOrder createUserOrder,
            IGetUserOrderByUserAndOrderId getUserOrderByUserAndOrderId,
            IAddNewMealsToUserOrders addNewMealsToUserOrders)
        {
            _getAllPubs = getAllPubs;
            _createNewPubOrder = createNewPubOrder;
            _createUserOrder = createUserOrder;
            _getUserOrderByUserAndOrderId = getUserOrderByUserAndOrderId;
            _addNewMealsToUserOrders = addNewMealsToUserOrders;
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

        [HttpGet("SubmitOrder/{OrderId}")]
        public IActionResult SubmitOrder(Guid OrderId)
        {
            ViewBag.OrderId = OrderId;
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

        [HttpPost("SubmitOrder/{OrderId}")]
        public IActionResult SubmitOrder(Guid OrderId, AddNewMealToOrderViewModel model)
        {
            var userOrderModel = _getUserOrderByUserAndOrderId.Invoke(ControllerContext.HttpContext.Actor(), OrderId);
            if (userOrderModel == null)
            {
                Alert.Danger("Please Join again. ");
                return RedirectToAction("SubmitOrder", "Order");
            }

            var addMealAction = _addNewMealsToUserOrders.Invoke(model.MealId, model.Quantity, userOrderModel);

            if (addMealAction == Guid.Empty)
            {
                Alert.Danger("Error, Please add again");
                return RedirectToAction("SubmitOrder", "Order");
            }
            Alert.Success("Your meal are added");
            return RedirectToAction("SubmitOrder", "Order");
        }
    }
}