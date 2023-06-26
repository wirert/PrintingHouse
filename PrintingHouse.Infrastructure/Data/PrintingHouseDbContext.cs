using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PrintingHouse.Infrastructure.Data.Entities;

namespace PrintingHouse.Infrastructure.Data
{
    /// <summary>
    /// Application database context
    /// </summary>
    public class PrintingHouseDbContext : IdentityDbContext<Employee>
    {
        public PrintingHouseDbContext(DbContextOptions<PrintingHouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Consumable> Consumables { get; set; } = null!;
        public DbSet<Machine> Machines { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<MachineArticle> MachinesArticles { get; set; } = null!;
        public DbSet<ArticleConsumable> ArticlesConsumables { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MachineArticle>().HasKey(k => new {k.MachineId, k.ArticleId});

            builder.Entity<ArticleConsumable>().HasKey(k => new {k.ArticleId, k.ConsumableId});

            base.OnModelCreating(builder);
        }
    }
}