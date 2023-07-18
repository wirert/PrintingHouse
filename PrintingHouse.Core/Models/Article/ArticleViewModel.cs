namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static Infrastructure.Constants.DataConstants;

    public class ArticleViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(Article.MaxNameLenght, MinimumLength = Article.MinNameLenght)]
        public string Name { get; set; } = null!;

        public IFormFile? DesignFile { get; set; }

        public string? DesignName { get; set; }

        [Required]
        public int MaterialId { get; set; }

        public string MaterialName { get; set; } = null!;

        [Required]
        public int ColorModelId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double MaterialQuantity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClientId { get; set; }

        public string ClientName { get; set; } = null!;

        public IList<AddArticleColorVeiwModel> Colors { get; set; } = new List<AddArticleColorVeiwModel>();
    }
}
