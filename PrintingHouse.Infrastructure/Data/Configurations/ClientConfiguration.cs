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
            builder.Property(c => c.ClientNumber).ValueGeneratedOnAdd();

            builder.HasData(new List<Client>
            {
                new Client()
                {
                    Id = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6"),
                    Name = "Test Client",
                    Email = "TestClient@email.com",
                    PhoneNumber = "1234567890",
                    MerchantId = 2
                },
                new Client()
                {
                    Id = Guid.Parse("cb76cf2f-c998-459a-83aa-46035256deea"),
                    Name = "Client 2",
                    Email = "client@email.com",
                    PhoneNumber = "+056568645",
                    MerchantId = 2
                },
                new Client()
                {
                    Id = Guid.Parse("46b7d975-1579-4dad-bdc6-f9dbd0232eab"),
                    Name = "Test test",
                    Email = "clienttest@email.com",
                    PhoneNumber = "+05656864545",
                    MerchantId = 2
                }
            });
        }
    }
}
