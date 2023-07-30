namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;
    using Entities.Enums;

    internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.Property(m => m.IsActive).HasDefaultValue(true);

            builder.HasData(CreateMaterials());
        }

        private ICollection<Material> CreateMaterials()
        {
            return new List<Material>()
            {
                new Material()
                {
                    Id = 1,
                    Type = "Plain paper A2",
                    MeasureUnit = MeasureUnit.Piece,
                    Width = 0.42d,
                    Lenght = 0.594d,
                    Price = 1m,
                    InStock = 10000
                },
                new Material()
                {
                    Id = 2,
                    Type = "Vinil 2m",
                    MeasureUnit = MeasureUnit.m,
                    Width = 2,
                    Lenght = 10,
                    Price = 1500.50m,
                    InStock = 100
                },
                new Material()
                {
                    Id = 3,
                    Type = "Nylon 20cm",
                    MeasureUnit = MeasureUnit.m,
                    Width = 0.2d,
                    Lenght = 1000,
                    Price = 850m,
                    InStock = 100
                }
            };
        }
    }
}
