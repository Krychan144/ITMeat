using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IPubOrderRepository : IGenericRepository<PubOrder>, IRepository
    {
        IQueryable<PubOrder> GetActiveOrders();

        IQueryable<PubOrder> GetUserSubmittedOrders(Guid userId);
    }
}