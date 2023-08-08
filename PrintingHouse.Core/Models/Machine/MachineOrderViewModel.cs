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
        
        public double MaterialPerPrint { get; set; }

        public MachineStatus Status { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; } = new Queue<OrderViewModel>();
    }
}
