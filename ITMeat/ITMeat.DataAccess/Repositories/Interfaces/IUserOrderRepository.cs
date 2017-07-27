using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IUserOrderRepository : IGenericRepository<UserOrder>, IRepository
    {
        IQueryable<Order> GetActiveUserOrders(Guid userid);
    }
}