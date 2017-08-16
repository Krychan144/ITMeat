using ITMeat.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace ITMeat.DataAccess.Context
{
    public class ITMeatDbContext : DbContext, IITMeatDbContext
    {
        public ITMeatDbContext()
        {
        }

        public ITMeatDbContext(DbContextOptions<ITMeatDbContext> option) : base(option)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ITMeat;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<MealType>().HasIndex(u => u.Name).IsUnique();
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void PerformMigration()
        {
            Database.Migrate();
        }

        #region DbSet

        public DbSet<User> Users { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserOrderMeal> UserOrderMeal { get; set; }
        public DbSet<MealType> MealType { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<Pub> Pub { get; set; }
        public DbSet<PubOrder> PubOrder { get; set; }

        #endregion DbSet
    }
}