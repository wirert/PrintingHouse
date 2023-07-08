namespace PrintingHouse.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Configurations;
    using Entities;
    using Entities.Account;

    /// <summary>
    /// Application database context
    /// </summary>
    public class PrintingHouseDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public PrintingHouseDbContext(DbContextOptions<PrintingHouseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Consumable> Consumables { get; set; } = null!;
        public virtual DbSet<Machine> Machines { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<ColorModel> ColorModels { get; set; } = null!;
        public virtual DbSet<ArticleConsumable> ArticlesConsumables { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ColorModelConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new ConsumableConfiguration());
            builder.ApplyConfiguration(new MachineConfiguration());

            builder.Entity<ArticleConsumable>().HasKey(k => new {k.ArticleId, k.ConsumableId});

            builder.Entity<Employee>().HasIndex(e => e.ApplicationUserId).IsUnique(); 
            
            builder.Entity<Client>().HasIndex(e => e.Name).IsUnique();            

            builder.Entity<Article>()
                .HasOne(a => a.Material)
                .WithMany(m => m.Articles)
                .HasForeignKey(a => a.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}