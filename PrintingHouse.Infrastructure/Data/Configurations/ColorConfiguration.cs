namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;

    internal class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasData(CreateConsumables());
        }

        private Color[] CreateConsumables()
        {
            return new Color[]
            {
                new Color()
                {
                    Id = 1,
                    Type = "Red",
                    Price = 50m,
                    InStock = 104
                },
                 new Color()
                {
                    Id = 2,
                    Type = "Green",
                    Price = 48m,
                    InStock = 92
                },
                  new Color()
                {
                    Id = 3,
                    Type = "Blue",
                    Price = 57m,
                    InStock = 67
                },
                   new Color()
                {
                    Id = 4,
                    Type = "Cyan",
                    Price = 52m,
                    InStock = 47
                },
                new Color()
                {
                    Id = 5,
                    Type = "Magenta",
                    Price = 55m,
                    InStock = 38
                },
                new Color()
                {
                    Id = 6,
                    Type = "Yellow",
                    Price = 47m,
                    InStock = 50
                },
                new Color()
                {
                    Id = 7,
                    Type = "Black",
                    Price = 40m,
                    InStock = 60
                }
            };
        }
    }
}
