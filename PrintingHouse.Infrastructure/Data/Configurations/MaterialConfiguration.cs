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
                    Width = 420,
                    Lenght = 594,
                    Price = 1m,
                    InStock = 10000
                },
                new Material()
                {
                    Id = 2,
                    Type = "Vinil 2m",
                    MeasureUnit = MeasureUnit.Km,
                    Width = 0.002,
                    Lenght = 0.01,
                    Price = 1500.50m,
                    InStock = 100
                },
                new Material()
                {
                    Id = 3,
                    Type = "Nylon 20cm",
                    MeasureUnit = MeasureUnit.Km,
                    Width = 0.0002,
                    Lenght = 1,
                    Price = 850m,
                    InStock = 20
                }
            };
        }
    }
}
