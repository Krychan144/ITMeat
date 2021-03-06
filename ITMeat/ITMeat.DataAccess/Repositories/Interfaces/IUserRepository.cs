﻿using System;
using System.Linq;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>, IRepository
    {
    }
}