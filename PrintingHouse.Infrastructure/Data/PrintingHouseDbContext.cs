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
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Machine> Machines { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<ColorModel> ColorModels { get; set; } = null!;
        public virtual DbSet<ArticleColor> ArticlesColors { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new ColorModelConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new ColorConfiguration());
            builder.ApplyConfiguration(new MachineConfiguration());

            builder.Entity<ArticleColor>().HasKey(k => new {k.ArticleId, k.ColorId});

            builder.Entity<ArticleColor>()
                .HasOne(ac => ac.Color)
                .WithMany(c => c.ArticleColors)
                .HasForeignKey(ac => ac.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArticleColor>()
                .HasOne(ac => ac.Article)
                .WithMany(a => a.ArticleColors)
                .HasForeignKey(ac => ac.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            
            
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