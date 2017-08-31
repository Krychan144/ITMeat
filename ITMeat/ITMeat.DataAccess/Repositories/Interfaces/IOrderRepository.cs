﻿using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>, IRepository
    {
        Order GetOrderByPubOrderId(Guid pubOrderId);

        IQueryable<UserOrderCountInPubs> GetUserOrderCountByPub(Guid userId);
    }
}