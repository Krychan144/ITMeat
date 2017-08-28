using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class AddsRepository : GenericRepository<Adds>, IAddsRepository
    {
        public AddsRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}