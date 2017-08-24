using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.Pub.Implementations
{
    public class EditPub : IEditPub
    {
        private readonly IPubRepository _pubRepository;

        public EditPub(IPubRepository pubRepository)
        {
            _pubRepository = pubRepository;
        }

        public bool Invoke(PubModel model)
        {
            if (model == null)
            {
                return false;
            }

            var pubToEdit = _pubRepository.GetById(model.Id);

            if (pubToEdit == null)
            {
                return false;
            }
            pubToEdit.Adress = model.Adress;
            pubToEdit.FreeDelivery = model.FreeDelivery;
            pubToEdit.Name = model.Name;
            pubToEdit.Phone = model.Phone;

            _pubRepository.Edit(pubToEdit);
            _pubRepository.Save();

            return true;
        }
    }
}