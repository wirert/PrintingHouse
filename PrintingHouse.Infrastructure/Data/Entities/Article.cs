using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PrintingHouse.Infrastructure.Data.Entities.Enums;
using static PrintingHouse.Infrastructure.Constants.DataConstants.Article;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Particular client article ready for print.")]
    public class Article
    {
        public Article()
        {
            ArticleConsumables = new HashSet<ArticleConsumable>();
            Orders = new HashSet<Order>();
            MachinesArticles = new HashSet<MachineArticle>();
        }

        [Comment("Article primary key.")]
        [Key]
        public int Id { get; set; }

        [Comment("Article name.")]
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; } = null!;

        [Comment("Article material")]
        [Required]
        public MaterialType Material { get; set; }

        [Comment("Article color model")]
        [Required]
        public ColorModel ColorModel { get; set; }

        [Comment("Article owner id")]
        [Required]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; } = null!;        

        public ICollection<ArticleConsumable> ArticleConsumables { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<MachineArticle> MachinesArticles { get; set; }

        [Comment("Soft delete boolean property")]
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
