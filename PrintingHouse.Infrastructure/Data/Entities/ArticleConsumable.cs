using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Article consumable with quantity (connecting table")]
    public class ArticleConsumable
    {
        [Comment("Article id")]
        [Required]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public Article Article { get; set; } = null!;

        [Comment("Consumable id")]
        [Required]
        public int ConsumableId { get; set; }

        [ForeignKey(nameof(ConsumableId))]
        public Consumable Consumable { get; set; } = null!;

        [Comment("Required consumable quantity for single print of article")]
        [Required]
        public double ConsumableQuantity { get; set; }
    }
}
