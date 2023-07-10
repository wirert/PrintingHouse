namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    using static Constants.DataConstants.Consumable;

    [Comment("Color")]
    public class Color
    {
        public Color()
        {
            ArticlesColors = new HashSet<ArticleColor>();
        }

        [Comment("Color primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Color type name")]
        [Required]
        [MaxLength(MaxTypeLenght)]
        public string Type { get; set; } = null!;

        [Comment("Color current quantit in stock")]
        [Required]
        public int InStock { get; set; }

        [Comment("Color price")]
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual ICollection<ArticleColor> ArticlesColors { get; set; }
    }
}
