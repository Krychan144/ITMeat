using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IPubRepository : IGenericRepository<Pub>, IRepository
    {
        Pub GetPubInfoByOrderId(Guid orderId);

        Pub GetPubByOrderId(Guid orderId);

        IQueryable<Meal> GetPubOferts(Guid pubId);
    }
}