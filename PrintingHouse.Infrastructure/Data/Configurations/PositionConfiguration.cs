namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;

    internal class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasData(CreatePositions());
        }

        private List<Position> CreatePositions()
        {
            return new List<Position>()
            {
                new Position 
                {
                    Id = 1,
                    Name = "Administrator"                    
                },
                 new Position
                {
                    Id = 2,
                    Name = "Merchant"
                },
                  new Position
                {
                    Id = 3,
                    Name = "Employee"
                },
                   new Position
                {
                    Id = 4,
                    Name = "Designer"
                },
                    new Position
                {
                    Id = 5,
                    Name = "Manager"
                },
                    new Position
                {
                    Id = 6,
                    Name = "Printer"
                }
            };
        }
    }
}
