using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>, IRepository
    {
        IQueryable<Order> GetOrderByPubOrderId(Guid pubOrderId);
    }
}