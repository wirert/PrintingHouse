using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using PrintingHouse.Infrastructure.Data.Entities.Enums;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Printing machine")]
    public class Machine
    {
        public Machine()
        {
            MachinesArticles = new HashSet<MachineArticle>();
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
        public DateTime PrintTime { get; set; }

        [Comment("Machine working color model")]
        [Required]
        public ColorModel ColorModel { get; set; }

        [Comment("Machine printing material id.")]
        [Required]
        public int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; } = null!;

        public ICollection<MachineArticle> MachinesArticles { get; set; }

        [Comment("Current status of the machine")]
        [Required]
        public MachineStatus Status { get; set; }

    }
}
