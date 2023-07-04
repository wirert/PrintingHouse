namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("Article consumable with quantity (connecting table")]
    public class ArticleConsumable
    {
        [Comment("Article id")]
        [Required]
        public Guid ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public virtual Article Article { get; set; } = null!;

        [Comment("Consumable id")]
        [Required]
        public int ConsumableId { get; set; }

        [ForeignKey(nameof(ConsumableId))]
        public virtual Consumable Consumable { get; set; } = null!;

        [Comment("Required consumable quantity for single print of article")]
        [Required]
        public double ConsumableQuantity { get; set; }
    }
}
