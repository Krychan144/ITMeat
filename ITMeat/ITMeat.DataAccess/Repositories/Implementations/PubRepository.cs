using System;
using System.Linq;
using ITMeat.DataAccess.Context;
using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ITMeat.DataAccess.Repositories.Implementations
{
    public class PubRepository : GenericRepository<Pub>, IPubRepository
    {
        public PubRepository(IITMeatDbContext context)
            : base(context)
        {
        }
    }
}