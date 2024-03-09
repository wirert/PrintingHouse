namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using Entities.Enums;
    using static Constants.DataConstants.Machine;

    [Comment("Printing machine")]
    public class Machine
    {
        public Machine()
        {
            OrdersQueue = new List<Order>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Printing machine name")]
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; } = null!;

        [Comment("Printing machine model (optional)")]
        [MaxLength(MaxModelLength)]
        public string? Model { get; set; }

        [Comment("Machine printing time for single unit")]
        [Required]
        public TimeSpan PrintTime { get; set; }

        [Comment("Foreign key to MaterialColorModel table")]
        [Required]
        public int MaterialId { get; set; }

        [Comment("Foreign key to MaterialColorModel table")]
        [Required]
        public int ColorModelId { get; set; }
                
        [Required]
        public virtual MaterialColorModel MaterialColorModel { get; set; } = null!;


        [Comment("Material required for single print")]
        [Required]
        public double MaterialPerPrint { get; set; }

        [Comment("Current status of the machine (has default value)")]
        [Required]
        public MachineStatus Status { get; set; }

        public virtual ICollection<Order> OrdersQueue { get; set; }
    }
}
