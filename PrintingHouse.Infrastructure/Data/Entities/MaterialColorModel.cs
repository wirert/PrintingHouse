namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("Machine material and color model connecting table with article colors")]
    public class MaterialColorModel
    {
        public MaterialColorModel()
        {
            Machines = new HashSet<Machine>();
            Articles = new HashSet<Article>();
        }

        [Comment("Material id (primary key)")]
        [Required]
        public int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public virtual Material Material { get; set; } = null!;

        [Comment("Color model id (primary key)")]
        [Required]
        public int ColorModelId { get; set; }

        [ForeignKey(nameof(ColorModelId))]
        public virtual ColorModel ColorModel { get; set; } = null!;

        public virtual ICollection<Machine> Machines { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
