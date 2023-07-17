namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    public class AddArticleColorVeiwModel
    {

        [Required]
        public int ColorId { get; set; }

        public string ColorName { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public double ColorQuantity { get; set; }
    }
}
