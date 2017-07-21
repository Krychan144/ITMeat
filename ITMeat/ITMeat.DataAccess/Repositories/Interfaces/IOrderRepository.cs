using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>, IRepository
    {
    }
}