namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;

    internal class MaterialColorModelConfiguration : IEntityTypeConfiguration<MaterialColorModel>
    {
        public void Configure(EntityTypeBuilder<MaterialColorModel> builder)
        {
            builder.HasKey(k => new { k.MaterialId, k.ColorModelId });

            builder
                .HasData(new MaterialColorModel[]
                        {
                            new MaterialColorModel()
                            {
                                MaterialId = 1,
                                ColorModelId = 1
                            },
                             new MaterialColorModel()
                            {
                                MaterialId = 2,
                                ColorModelId = 2
                            },
                              new MaterialColorModel()
                            {
                                MaterialId = 3,
                                ColorModelId = 2
                            },
                        });
        }
    }
}
