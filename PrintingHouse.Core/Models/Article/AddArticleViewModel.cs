namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    using ColorModel;
    using Material;

    using static Infrastructure.Constants.DataConstants.Article;

    public class AddArticleViewModel
    {
        [Required]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(MaxImageNameLenght, MinimumLength = MinImageNameLenght)]
        public string ImageName { get; set; } = null!;

        [Required]
        public int MaterialId { get; set; }

        public IEnumerable<MaterialSelectViewModel> Materials { get; set; } = Enumerable.Empty<MaterialSelectViewModel>();

        [Required]
        public int ColorModelId { get; set; }

        public IEnumerable<ColorModelSelectViewModel> ColorModels { get; set; } = Enumerable.Empty<ColorModelSelectViewModel>();

        [Required]
        [Range(0, double.MaxValue)]
        public double MaterialQuantity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClientId { get; set; }

        public string ClientName { get; set; } = null!;

        public ICollection<AddArticleColorVeiwModel> ArticleColors { get; set; } = new HashSet<AddArticleColorVeiwModel>();
    }
}
