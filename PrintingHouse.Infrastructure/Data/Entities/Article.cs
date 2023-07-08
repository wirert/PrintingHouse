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
            ArticleConsumables = new HashSet<ArticleConsumable>();
            Orders = new HashSet<Order>();
        }

        [Comment("Article primary key.")]
        [Key]
        public Guid Id { get; set; }

        [Comment("Article name.")]
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; } = null!;

        [Comment("Name of design image")]
        [Required]
        [MaxLength(MaxImageNameLenght)]
        public string ImageName { get; set; } = null!;

        [Comment("Article material")]
        [Required]
        public int MaterialId { get; set; }

        [Required]
        [ForeignKey(nameof(MaterialId))]
        public virtual Material Material { get; set; } = null!;

        [Comment("Required material lenght")]
        [Required]
        public double RequiredMaterial { get; set; }

        [Comment("Article color model id")]
        [Required]
        public int ColorModelId { get; set; }

        [ForeignKey(nameof(ColorModelId))]
        [Required]
        public virtual ColorModel ColorModel { get; set; } = null!;

        [Comment("Article owner id")]
        [Required]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; } = null!;

        public virtual ICollection<ArticleConsumable> ArticleConsumables { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [Comment("Soft delete boolean property")]
        public bool IsActive { get; set; } = true;
    }
}
