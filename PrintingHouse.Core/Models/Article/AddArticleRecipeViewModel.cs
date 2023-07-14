namespace PrintingHouse.Core.Models.Article
{
    public class AddArticleRecipeViewModel
    {
        public string ArticleName { get; set; } = null!;

        public string ClientName { get; set; } = null!;

        public int ColorModelId { get; set; }

        public IList<AddArticleColorVeiwModel> Colors { get; set; } = new List<AddArticleColorVeiwModel>();
    }
}
