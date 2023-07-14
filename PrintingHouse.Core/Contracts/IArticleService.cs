namespace PrintingHouse.Core.Contracts
{
    using Models.Article;

    public interface IArticleService
    {
        Task AddAsync(AddArticleViewModel model);

        Task<AddArticleViewModel> FillAddModelWithDataAsync(AddArticleViewModel model);

        Task AddColorRecipeAsync(IList<AddArticleColorVeiwModel> colorRecipes);
    }
}
