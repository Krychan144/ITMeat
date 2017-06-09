using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using ITMeat.BusinessLogic.Configuration.Interfaces;
using ITMeat.DataAccess.Context;

namespace ITMeat.BusinessLogic.Configuration.Implementations
{
    public class MigrationHelper : IMigrationHelper
    {
        private readonly IITMeatDbContext dbContext;

        public MigrationHelper(IITMeatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Migrate()
        {
            dbContext.PerformMigration();
        }
    }
}