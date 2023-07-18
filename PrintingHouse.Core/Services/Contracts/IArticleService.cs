namespace PrintingHouse.Core.Services.Contracts
{
    using Models.Article;

    public interface IArticleService
    {
        Task<IEnumerable<AllArticleViewModel>> GetAllAsync(int? id);

        Task CreateAsync(ArticleViewModel model);

        Task<ChooseArticleMaterialAndColorsViewModel> FillAddModelWithDataAsync(ChooseArticleMaterialAndColorsViewModel model);

        Task<bool> ExistByIdAsync(Guid? id);

        Task<ArticleViewModel> GetByIdAsync(Guid id);

        Task EditAsync(ArticleViewModel model);
    }
}
