namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PrintingHouse.Infrastructure.Data.Entities;
    using System;

    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(a => a.IsActive).HasDefaultValue(true);
            builder.HasIndex(e => e.Name).IsUnique();

            builder.HasData(new List<Client>
            {
                new Client()
                {
                    Id = 101,
                    Name = "Test Client",
                    Email = "TestClient@email.com",
                    PhoneNumber = "1234567890",
                    MerchantId = 2
                },
                new Client()
                {
                    Id = 102,
                    Name = "Client 2",
                    Email = "client@email.com",
                    PhoneNumber = "+056568645",
                    MerchantId = 2
                }
            });
        }
    }
}
