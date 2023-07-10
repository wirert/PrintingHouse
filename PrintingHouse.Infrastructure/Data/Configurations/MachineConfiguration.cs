namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using PrintingHouse.Infrastructure.Data.Entities;
    using PrintingHouse.Infrastructure.Data.Entities.Enums;

    internal class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.HasOne(m => m.MaterialColorModel)
                .WithMany(mc => mc.Machines)
                .HasForeignKey(m => new {m.MaterialId, m.ColorModelId });

            builder.Property(p => p.Status).HasDefaultValue(MachineStatus.Working);

           builder.HasData(CreateMachines());
        }

        private List<Machine> CreateMachines()
        {
            var machines = new List<Machine>
            {
                new Machine()
                {
                    Id = 1,
                    Name = "Machine 1",
                    ColorModelId = 2,
                    MaterialId = 2,
                    MaterialPerPrint = 5,
                    PrintTime = TimeSpan.FromMinutes(3),
                    Status = MachineStatus.Working
                },
                new Machine()
                {
                    Id = 2,
                    Name = "Machine 2",
                    ColorModelId = 2,
                    MaterialId = 2,
                    MaterialPerPrint = 5,
                    PrintTime = TimeSpan.FromSeconds(150),
                    Status = MachineStatus.Working
                },
                new Machine()
                {
                    Id = 3,
                    Name = "Machine 3",
                    MaterialId = 1,
                    ColorModelId = 1,
                    MaterialPerPrint = 1,
                    PrintTime = TimeSpan.FromSeconds(3),
                    Status = MachineStatus.Working
                },
                new Machine()
                {
                    Id = 4,
                    Name = "Machine 4",
                    MaterialId = 3,
                    ColorModelId = 2,
                    MaterialPerPrint = 1,
                    PrintTime = TimeSpan.FromMinutes(40),
                    Status = MachineStatus.Working
                }
            };

            return machines;
        }
    }
}
