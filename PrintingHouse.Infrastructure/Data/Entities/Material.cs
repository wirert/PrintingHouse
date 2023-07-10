namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using Entities.Enums;
    using static Constants.DataConstants.Material;

    [Comment("Мaterial on which it is printed")]
    public class Material
    {
        public Material()
        {
            MachineMaterials = new HashSet<MaterialColorModel>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Material type name")]
        [Required]
        [MaxLength(MaxTypeLenght)]
        public string Type { get; set; } = null!;

        [Comment("Material width")]
        [Required]
        public double Width { get; set; }

        [Comment("Material lenght")]
        [Required]
        public double Lenght { get; set; }

        [Comment("Material measure unit (enumeration)")]
        [Required]
        public MeasureUnit MeasureUnit { get; set; }

        [Comment("Material price")]
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Comment("Material current quantit in stock")]
        [Required]
        public int InStock { get; set; }

        public virtual ICollection<MaterialColorModel> MachineMaterials { get; set; }   

        [Comment("Soft delete property")]
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
