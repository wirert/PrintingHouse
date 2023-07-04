namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using Entities.Enums;

    [Comment("Printing machine")]
    public class Machine
    {
        public Machine()
        {
           Status = MachineStatus.Working;
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Printing machine name")]
        [Required]
        public string Name { get; set; } = null!;

        [Comment("Printing machine model (optional")]
        public string? Model { get; set; }

        [Comment("Machine printing time for single unit")]
        [Required]
        public TimeSpan PrintTime { get; set; }

        [Comment("Machine working color model id")]
        [Required]
        public int ColorModelId { get; set; }

        [ForeignKey(nameof(ColorModelId))]
        [Required]
        public virtual ColorModel ColorModel { get; set; } = null!;

        [Comment("Machine printing material id.")]
        [Required]
        public int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public virtual Material Material { get; set; } = null!;

        [Comment("Material required for single print")]
        [Required]
        public double MaterialPerPrint { get; set; }

        [Comment("Current status of the machine (has default value)")]
        [Required]
        public MachineStatus Status { get; set; }

    }
}
