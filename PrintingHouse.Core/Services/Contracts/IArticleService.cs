namespace PrintingHouse.Core.Services.Contracts
{
    using Models.Article;

    public interface IArticleService
    {
        Task<IEnumerable<AllArticleViewModel>> GetAllAsync(int? id);

        Task CreateAsync(CreateArticleViewModel model);

        Task<ChooseArticleMaterialAndColorsViewModel> FillAddModelWithDataAsync(ChooseArticleMaterialAndColorsViewModel model);
    }
}
