namespace PrintingHouse.Core.Services
{
    using System.Threading.Tasks;

    using Contracts;
    using Models.Article;
    using Infrastructure.Data.Common.Contracts;

    public class ArticleService : IArticleService
    {
        private readonly IRepository repo;

        public ArticleService(IRepository _repo)
        {
            repo = _repo;
        }

        public Task AddAsync(AddArticleViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
