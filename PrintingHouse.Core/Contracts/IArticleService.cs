namespace PrintingHouse.Core.Contracts
{
    using Models.Article;

    public interface IArticleService
    {
        Task AddAsync(AddArticleViewModel model);

        Task<AddArticleViewModel> FillAddModelWithData(AddArticleViewModel model);
    }
}
