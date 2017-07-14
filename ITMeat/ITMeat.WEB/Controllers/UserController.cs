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

namespace ITMeat.WEB.Controllers
{
    public class UserController : BaseController
    {
        private readonly IGetAllPubs _getAllPubs;
        private readonly IAddNewPub _addNewPub;
        private readonly ICreateNewOrder _createNewOrder;

        public UserController(IGetAllPubs getAllPubs,
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
        public IActionResult NewOrder(AddNewOrderViewModel model)
        {
            var pubList = _getAllPubs.Invoke();

            model.Pubs = new List<SelectListItem>();

            foreach (var item in pubList)
            {
                model.Pubs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            return View(model);
        }

        [HttpPost("NewOrder")]
        public IActionResult NewOrder(CreateNewOrderViewModel model)
        {
            var userModel = new UserModel { Id = ControllerContext.HttpContext.Actor() };
            var orderModel = new OrderModel { Name = model.PubName, EndDateTime = model.EndOrders, CreatedOn = DateTime.Now, Owner = userModel };

            var orderAddAction = _createNewOrder.Invoke(orderModel, ControllerContext.HttpContext.Actor());

            if (orderAddAction == Guid.Empty)
            {
                Alert.Danger("Zamowienie już istnieje");
                return View();
            }
            Alert.Success("Great, Order are create.");
            return RedirectToAction("Index", "User");
        }
    }
}