namespace PrintingHouse.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Configurations;
    using Entities;
    using Entities.Account;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;

    /// <summary>
    /// Application database context
    /// </summary>
    public class PrintingHouseDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        //Only for testing!! Shoud be removed.
        private readonly IEntityTypeConfiguration<Article> articleConfig;

        public PrintingHouseDbContext(DbContextOptions<PrintingHouseDbContext> options, IEntityTypeConfiguration<Article> _articleConfig)
            : base(options)
        {     
            articleConfig = _articleConfig;
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
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new ColorConfiguration());
            builder.ApplyConfiguration(new ColorModelConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new MaterialColorModelConfiguration());
            builder.ApplyConfiguration(new MachineConfiguration());

            //Added only for test data seeding in db
            // !! Should be removed !!
            builder.ApplyConfiguration(articleConfig);
            builder.ApplyConfiguration(new ArticleColorConfiguration());


            builder.Entity<Article>().Property(a => a.IsActive).HasDefaultValue(true);
            builder.Entity<Article>()
                .HasOne(m => m.MaterialColorModel)
                .WithMany(mc => mc.Articles)
                .HasForeignKey(m => new { m.MaterialId, m.ColorModelId });                 

            builder.Entity<ArticleColor>().HasKey(k => new {k.ArticleId, k.ColorId});            

            base.OnModelCreating(builder);
        }
    }
}