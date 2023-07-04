namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;

    internal class ConsumableConfiguration : IEntityTypeConfiguration<Consumable>
    {
        public void Configure(EntityTypeBuilder<Consumable> builder)
        {
            builder.HasData(CreateConsumables());
        }

        private Consumable[] CreateConsumables()
        {
            return new Consumable[]
            {
                new Consumable()
                {
                    Id = 1,
                    Type = "Red",
                    Price = 50m,
                    InStock = 104
                },
                 new Consumable()
                {
                    Id = 2,
                    Type = "Green",
                    Price = 48m,
                    InStock = 92
                },
                  new Consumable()
                {
                    Id = 3,
                    Type = "Blue",
                    Price = 57m,
                    InStock = 67
                },
                   new Consumable()
                {
                    Id = 4,
                    Type = "Cyan",
                    Price = 52m,
                    InStock = 47
                }, new Consumable()
                {
                    Id = 5,
                    Type = "Magenta",
                    Price = 55m,
                    InStock = 38
                }, new Consumable()
                {
                    Id = 6,
                    Type = "Yellow",
                    Price = 47m,
                    InStock = 50
                }, new Consumable()
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
