using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.WEB.Models.Pub;
using ITMeat.WEB.Models.Pub.FormModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITMeat.WEB.Controllers
{
    [Route("Pub")]
    public class PubController : BaseController
    {
        private readonly IGetAllPubs _getAllPubs;
        private readonly IAddNewPub _addNewPub;
        private readonly IGetPubInfoById _getPubInfoById;
        private readonly IEditPub _editPub;
        private readonly IGetPubOferts _getPubOferts;

        public PubController(IGetAllPubs getAllPubs,
            IAddNewPub addNewPub,
            IGetPubInfoById getPubInfoById,
            IEditPub editPub,
            IGetPubOferts getPubOferts)
        {
            _getAllPubs = getAllPubs;
            _addNewPub = addNewPub;
            _getPubInfoById = getPubInfoById;
            _editPub = editPub;
            _getPubOferts = getPubOferts;
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
            return RedirectToAction("AddNewPub", "Pub");
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
            return RedirectToAction("EditPubInformations", "Pub", new { model.PubId });
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
                return RedirectToAction("EditPubInformations", "Pub", new { model.PubId });
            }
            Alert.Danger("Error. Pub are not edited");
            return RedirectToAction("EditPubInformations", "Pub", new { model.PubId });
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
    }
}