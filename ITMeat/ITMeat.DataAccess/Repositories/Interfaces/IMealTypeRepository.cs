﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.DataAccess.Models;

namespace ITMeat.DataAccess.Repositories.Interfaces
{
    public interface IMealTypeRepository : IGenericRepository<MealType>, IRepository
    {
        IQueryable<MealType> GetMealTypes (Guid pubId);
    }
}