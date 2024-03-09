namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using static Constants.DataConstants.Article;

    [Comment("Particular client article ready for print.")]
    public class Article
    {
        public Article()
        {
            Id = Guid.NewGuid();
            ArticleColors = new HashSet<ArticleColor>();
            Orders = new HashSet<Order>();
        }

        [Comment("Article primary key.")]
        [Key]
        public Guid Id { get; set; }

        [Comment("Article custom number")]
        [Required]
        [MaxLength(MaxArticleNoLength)]
        public string ArticleNumber { get; set; } = null!;

        [Comment("Article name.")]
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; } = null!;

        [Comment("Name of design image")]
        [Required]
        [MaxLength(MaxImageNameLength)]
        public string ImageName { get; set; } = null!;

        [Comment("Article length")]
        [Required]
        public double Length { get; set; }       

        [Comment("Article owner id")]
        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; } = null!;

        [Comment("Foreign key to MaterialColorModel table")]
        [Required]
        public int MaterialId { get; set; }

        [Comment("Foreign key to MaterialColorModel table")]
        [Required]
        public int ColorModelId { get; set; }

        [Required]
        public virtual MaterialColorModel MaterialColorModel { get; set; } = null!;

        public virtual ICollection<ArticleColor> ArticleColors { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [Comment("Soft delete boolean property")]
        public bool IsActive { get; set; } = true;
    }
}
