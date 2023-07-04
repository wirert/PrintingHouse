namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;

    internal class ColorModelConfiguration : IEntityTypeConfiguration<ColorModel>
    {
        public void Configure(EntityTypeBuilder<ColorModel> builder)
        {
            builder.HasData(CreateColorModels());
        }

        private List<ColorModel> CreateColorModels()
        {
            return new List<ColorModel>()
            {
                new ColorModel
                {
                    Id = 1,
                    Name = "RGB"
                },
                 new ColorModel
                {
                    Id = 2,
                    Name = "CMYK"
                }
            };
        }
    }
}
