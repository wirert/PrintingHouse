namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    public class AddArticleColorVeiwModel
    {
        public string ArticleName { get; set; } = null!;

        public string ClientName { get; set; } = null!;

        public int ColorModelId { get; set; }

        [Required]
        public int ColorId { get; set; }

        public string ColorName { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public double ColorQuantity { get; set; }
    }
}
