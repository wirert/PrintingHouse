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

           // builder.HasData(CreateMachines());
        }

        private List<Machine> CreateMachines()
        {
            var materialColorModel = new MaterialColorModel()
            {
                ColorModelId = 2,
                MaterialId = 2
            };


            var machines = new List<Machine>();

            machines.Add(new Machine()
            {
                Id = 1,
                Name = "Machine 1",
                MaterialColorModel = materialColorModel,                
                MaterialPerPrint = 5,
                PrintTime = TimeSpan.FromMinutes(3),
                Status = MachineStatus.Working
            });

            machines.Add(new Machine()
            {
                Id = 2,
                Name = "Machine 2",
                MaterialColorModel = materialColorModel,
                MaterialPerPrint = 5,
                PrintTime = TimeSpan.FromSeconds(150),
                Status = MachineStatus.Working
            });

            materialColorModel = new MaterialColorModel()
            {
                MaterialId = 1,
                ColorModelId = 1
            };

            machines.Add(new Machine()
            {
                Id = 3,
                Name = "Machine 3",
                MaterialColorModel = materialColorModel,
                MaterialPerPrint = 1,
                PrintTime = TimeSpan.FromSeconds(3),
                Status = MachineStatus.Working
            });

            materialColorModel = new MaterialColorModel()
            {
                MaterialId = 3,
                ColorModelId = 2
            };
            machines.Add(new Machine()
            {
                Id = 4,
                Name = "Machine 4",
                MaterialColorModel = materialColorModel,
                MaterialPerPrint = 1,
                PrintTime = TimeSpan.FromMinutes(40),
                Status = MachineStatus.Working
            });

            return machines;
        }
    }
}
