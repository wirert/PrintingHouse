namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        [Range(0, double.MaxValue)]
        public double MaterialQuantity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClientId { get; set; }

        //IEnumerable<>
    }
}
