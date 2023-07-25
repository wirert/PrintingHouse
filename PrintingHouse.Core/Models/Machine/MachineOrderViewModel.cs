namespace PrintingHouse.Core.Models.Machine
{
    using Order;
    using Infrastructure.Data.Entities.Enums;

    public class MachineOrderViewModel
    {
        public int Id { get; set; }
                
        public string Name { get; set; } = null!;
                
        public string? Model { get; set; }
                
        public TimeSpan PrintTime { get; set; }

        public string Material { get; set; } = null!;

        public string ColorModel { get; set; } = null!;
        
        public double MaterialPerPrint { get; set; }

        public MachineStatus Status { get; set; }

        public virtual IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}
