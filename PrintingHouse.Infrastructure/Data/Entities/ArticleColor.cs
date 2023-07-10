namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    [Comment("Article color with required quantity")]
    public class ArticleColor
    {
        [Comment("Article id")]
        [Required]
        public Guid ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public virtual Article Article { get; set; } = null!;

        [Comment("Color model id")]
        [Required]
        public int ColorModelId { get; set; }

        [ForeignKey(nameof(ColorModelId))]
        public virtual ColorModel ColorModel { get; set; } = null!;

        [Comment("Color id")]
        [Required]
        public int ColorId { get; set; }

        [ForeignKey(nameof(ColorId))]
        public virtual Color Color { get; set; } = null!;

        [Comment("Required color quantity for single print of article")]
        [Required]
        public double ColorQuantity { get; set; }
    }
}
