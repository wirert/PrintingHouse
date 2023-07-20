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
        public virtual DbSet<ArticleColor> ArticleColors { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ColorModel> ColorModels { get; set; } = null!;
        public virtual DbSet<ColorModel> Colors { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialColorModel> MaterialsColorModels { get; set; } = null!;
        public virtual DbSet<Machine> Machines { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new ColorConfiguration());
            builder.ApplyConfiguration(new ColorModelConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new MaterialColorModelConfiguration());
            builder.ApplyConfiguration(new MachineConfiguration());

            builder.Entity<Article>().Property(a => a.IsActive).HasDefaultValue(true);
            builder.Entity<Article>()
                .HasOne(m => m.MaterialColorModel)
                .WithMany(mc => mc.Articles)
                .HasForeignKey(m => new { m.MaterialId, m.ColorModelId });

            builder.Entity<Client>().Property(a => a.IsActive).HasDefaultValue(true);
            builder.Entity<Client>().HasIndex(e => e.Name).IsUnique();            

            builder.Entity<ArticleColor>().HasKey(k => new {k.ArticleId, k.ColorId});

            builder.Entity<Order>().HasOne(o => o.Machine)
                .WithMany(m => m.OrdersQueue)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}