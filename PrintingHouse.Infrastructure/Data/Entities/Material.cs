using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using PrintingHouse.Infrastructure.Data.Entities.Enums;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Мaterial on which it is printed")]
    public class Material
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Material type (enumeration)")]
        [Required]
        public MaterialType Type { get; set; }

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
        public decimal Price { get; set; }

        [Comment("Material current quantit in stock")]
        [Required]
        public int InStock { get; set; }
    }
}
