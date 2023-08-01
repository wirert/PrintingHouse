namespace PrintingHouse.Core.Models.Article
{
    using PrintingHouse.Core.Models.ColorModel;
    using PrintingHouse.Core.Models.Material;

    public class ChooseArticleMaterialAndColorsViewModel
    {
        public Guid ClientId { get; set; }

        public string ClientName { get; set; } = null!;

        public int MaterialId { get; set; }

        public int ColorModelId { get; set; }

        public Guid? ArticleId { get; set; }

        public ICollection<MaterialSelectViewModel> Materials { get; set; } = new HashSet<MaterialSelectViewModel>();

        public ICollection<ColorModelSelectViewModel> ColorModels { get; set; } = new HashSet<ColorModelSelectViewModel>();

    }
}
