namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    using ColorModel;
    using Material;
    using Microsoft.AspNetCore.Http;
    using static Infrastructure.Constants.DataConstants.Article;

    public class AddArticleViewModel
    {
        [Required]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght)]
        public string Name { get; set; } = null!;

        
        public IFormFile DesignFile { get; set; } = null!;

        [Required]
        public int MaterialId { get; set; }

        public ICollection<MaterialSelectViewModel> Materials { get; set; } = new HashSet<MaterialSelectViewModel>();

        [Required]
        public int ColorModelId { get; set; }

        public ICollection<ColorModelSelectViewModel> ColorModels { get; set; } = new HashSet<ColorModelSelectViewModel>();

        [Required]
        [Range(0, double.MaxValue)]
        public double MaterialQuantity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClientId { get; set; }

        public string? ClientName { get; set; }       
    }
}
