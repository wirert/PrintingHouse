namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;
    using Entities.Enums;

    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(SeedOrders());
        }

        private List<Order> SeedOrders()
        {
            return new List<Order>()
            {
                new Order()
                {
                    Id = Guid.Parse("54b5df45-d161-45c6-a6ba-f0c37d5667b6"),
                    ArticleId = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"),
                    Comment = "",
                    ExpectedPrintDate = DateTime.Now.Date,
                    ExpectedPrintDuration = TimeSpan.FromMinutes(5),
                    MachineId = 3,
                    OrderTime = DateTime.UtcNow,
                    Quantity = 100,
                    Status = OrderStatus.Waiting                    
                },
                new Order()
                {
                    Id = Guid.Parse("5a167a8c-5136-4c74-90b2-2c958ae54b20"),
                    ArticleId = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"),
                    Comment = "",
                    ExpectedPrintDate = DateTime.Now.Date,
                    ExpectedPrintDuration = TimeSpan.FromMinutes(50),
                    MachineId = 3,
                    OrderTime = DateTime.UtcNow.AddMinutes(35),
                    Quantity = 1000,
                    Status = OrderStatus.NoConsumable,
                    EndDate = DateTime.UtcNow.Date.AddDays(10)
                },
                new Order()
                {
                    Id = Guid.Parse("17e2de12-142f-47c7-9af8-3b138eb9cfbf"),
                    ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                    Comment = "",
                    ExpectedPrintDate = DateTime.Now.Date,
                    ExpectedPrintDuration = TimeSpan.FromMinutes(54),
                    MachineId = 1,
                    OrderTime = DateTime.UtcNow,
                    Quantity = 20,
                    Status = OrderStatus.Waiting,
                },
                new Order()
                {
                    Id = Guid.Parse("ca5b12c6-f0cb-41a9-83db-be95e5509a43"),
                    ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                    Comment = "Test",
                    ExpectedPrintDate = DateTime.Now.Date,
                    ExpectedPrintDuration = TimeSpan.FromMinutes(45),
                    MachineId = 2,
                    OrderTime = DateTime.UtcNow.AddMinutes(5),
                    Quantity = 20,
                    Status = OrderStatus.NoConsumable,
                },
                new Order()
                {
                    Id = Guid.Parse("8d93617f-ec76-4831-8dd6-73e78eb821b4"),
                    ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                    Comment = "Test 2",
                    ExpectedPrintDate = DateTime.Now.Date,
                    ExpectedPrintDuration = TimeSpan.FromMinutes(90),
                    MachineId = 2,
                    OrderTime = DateTime.UtcNow.AddMinutes(10),
                    Quantity = 40,
                    Status = OrderStatus.Waiting,
                },
                new Order()
                {
                    Id = Guid.Parse("b572ce54-5707-426f-b8a9-2d765bc415e4"),
                    ArticleId = Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"),
                    Comment = "",
                    ExpectedPrintDate = DateTime.Now.Date.AddDays(1),
                    ExpectedPrintDuration = TimeSpan.FromMinutes(40),
                    MachineId = 4,
                    OrderTime = DateTime.UtcNow,
                    Quantity = 10000,
                    Status = OrderStatus.Waiting,
                },
            };
        }
    }
}
