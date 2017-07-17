using System;
using System.Collections.Generic;
using System.Security.Claims;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Implementations;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Models.Order;
using Microsoft.AspNetCore.Mvc;
using ITMeat.WEB.Models.Pub;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using ITMeat.WEB.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ITMeat.WEB.Controllers
{
    [Route("Order")]
    public class OrderController : BaseController
    {
        private readonly IGetAllPubs _getAllPubs;
        private readonly IAddNewPub _addNewPub;
        private readonly ICreateNewOrder _createNewOrder;

        public OrderController(IGetAllPubs getAllPubs,
            IAddNewPub addNewPub,
            ICreateNewOrder createNewOrder)
        {
            _getAllPubs = getAllPubs;
            _addNewPub = addNewPub;
            _createNewOrder = createNewOrder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("NewOrder")]
        public IActionResult NewOrder()
        {
            var pubList = _getAllPubs.Invoke();
            var model = new AddNewOrderViewModel();
            model.Pubs = new List<SelectListItem>();

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
            var orderModel = new OrderModel { PubId = new Guid(model.PubId), EndDateTime = model.EndOrders, CreatedOn = DateTime.Now, Owner = userModel };

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
    }
}