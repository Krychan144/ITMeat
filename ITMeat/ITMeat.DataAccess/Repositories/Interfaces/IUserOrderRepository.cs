using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IUserOrderRepository : IGenericRepository<UserOrder>, IRepository
    {
        IQueryable<UserOrder> GetUserOrders(Guid pubOrderId, Guid userOrderId);

        UserOrder GetUserOrderByUserAndOrderId(Guid userId, Guid orderId);
    }
}