using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>, IRepository
    {
        Order GetOrderByPubOrderId(Guid pubOrderId);

        IQueryable<Order> GetUserOrders(Guid userId);

        IQueryable<Order> GetActiveUserOrders(Guid userid);
    }
}