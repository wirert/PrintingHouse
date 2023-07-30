namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;

    internal class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasOne(c => c.ColorModel)
                .WithMany(cm => cm.Colors)
                .OnDelete(DeleteBehavior.NoAction);

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
                    InStock = 250,
                    ColorModelId = 1
                },
                 new Color()
                {
                    Id = 2,
                    Type = "Green",
                    Price = 48m,
                    InStock = 300,
                    ColorModelId = 1
                },
                  new Color()
                {
                    Id = 3,
                    Type = "Blue",
                    Price = 57m,
                    InStock = 280,
                    ColorModelId = 1
                },
                   new Color()
                {
                    Id = 4,
                    Type = "Cyan",
                    Price = 52m,
                    InStock = 180,
                    ColorModelId = 2
                },
                new Color()
                {
                    Id = 5,
                    Type = "Magenta",
                    Price = 55m,
                    InStock = 200,
                    ColorModelId = 2
                },
                new Color()
                {
                    Id = 6,
                    Type = "Yellow",
                    Price = 47m,
                    InStock = 200,
                    ColorModelId = 2
                },
                new Color()
                {
                    Id = 7,
                    Type = "Black",
                    Price = 40m,
                    InStock = 230,
                    ColorModelId = 2
                }
            };
        }
    }
}
