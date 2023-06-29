using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using static PrintingHouse.Infrastructure.Constants.DataConstants.Consumable;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Machine consumable")]
    public class Consumable
    {
        public Consumable()
        {
            ArticleConsumables = new List<ArticleConsumable>();
        }

        [Comment("Consumable primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Consumable type name")]
        [Required]
        [MaxLength(MaxTypeLenght)]
        public string Type { get; set; } = null!;

        [Comment("Consumable current quantit in stock")]
        [Required]
        public int InStock { get; set; }

        [Comment("Consumable price")]
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public ICollection<ArticleConsumable> ArticleConsumables { get; set; }

    }
}
