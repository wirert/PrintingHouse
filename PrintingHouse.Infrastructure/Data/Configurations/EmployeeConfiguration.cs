namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PrintingHouse.Infrastructure.Data.Entities;

    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasIndex(e => e.ApplicationUserId).IsUnique();

            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasData(CreateEmployees());
        }

        private List<Employee> CreateEmployees()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    ApplicationUserId = Guid.Parse("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                    PositionId = 1                    
                }
            };
        }
    }
}
