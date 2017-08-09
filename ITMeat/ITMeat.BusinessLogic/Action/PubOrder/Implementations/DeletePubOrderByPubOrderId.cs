using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.PubOrder.Implementations
{
    public class DeletePubOrderByPubOrderId : IDeletePubOrderByPubOrderId
    {
        private readonly IPubOrderRepository _pubOrderRepository;

        public DeletePubOrderByPubOrderId(IPubOrderRepository pubOrderRepository)
        {
            _pubOrderRepository = pubOrderRepository;
        }

        public bool Invoke(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var pubOrderToDelete = _pubOrderRepository.GetById(id);
            if (pubOrderToDelete == null)
            {
                return false;
            }
            _pubOrderRepository.Delete(pubOrderToDelete);
            _pubOrderRepository.Save();
            return true;
        }
    }
}