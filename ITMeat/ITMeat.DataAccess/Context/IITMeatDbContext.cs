using ITMeat.DataAccess.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITMeat.DataAccess.Context
{
    public interface IITMeatDbContext : IRepository
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        void SetModified(object entity);

        void PerformMigration();
    }
}