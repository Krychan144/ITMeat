using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ITMeat.WEB.Controllers
{
    [Route("Order")]
    public class OrderController : BaseController
    {
        private readonly IGetAllPubs _getAllPubs;
        private readonly ICreateNewOrder _createNewOrder;
        private readonly IGetActiveOrders _getActiveOrders;

        public OrderController(IGetAllPubs getAllPubs,
            ICreateNewOrder createNewOrder,
            IGetActiveOrders getActiveOrders)
        {
            _getAllPubs = getAllPubs;
            _createNewOrder = createNewOrder;
            _getActiveOrders = getActiveOrders;
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
            var userModel = new UserModel { Id = ControllerContext.HttpContext.Actor() };
            var orderModel = new OrderModel { PubId = model.PubId, EndDateTime = model.EndOrders, CreatedOn = DateTime.UtcNow, Owner = userModel };

            var orderAddAction = _createNewOrder.Invoke(orderModel, ControllerContext.HttpContext.Actor());

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

        [HttpGet("SubmitOrder")]
        public IActionResult SubmitOrder()
        {
            return View();
        }

        [HttpGet("OrdersHistory")]
        public IActionResult OrdersHistory()
        {
            return View();
        }
    }
}