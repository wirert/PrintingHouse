namespace PrintingHouse.Core.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Article;
    using Models.ColorModel;
    using Models.Material;
    using Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities;
    using System.Collections.Generic;

    public class ArticleService : IArticleService
    {
        private readonly IRepository repo;
        private readonly IFileService fileService;

        public ArticleService(
            IRepository _repo, 
            IFileService _fileService)
        {
            repo = _repo;
            fileService = _fileService;
        }

        public async Task AddAsync(AddArticleViewModel model)
        {
            var article = new Article()
            { 
                Name = model.Name,
                ClientId = model.ClientId,
                MaterialQuantity = model.MaterialQuantity,
                ImageName = model.DesignFile.FileName                
            };

            await fileService.SaveFileAsync(article.Id, model.DesignFile.FileName, model.DesignFile);

            await repo.AddAsync(article);
            await repo.SaveChangesAsync();
        }

        public async Task AddColorRecipeAsync(IList<AddArticleColorVeiwModel> model)
        {
            var article = await repo
                .All<Article>(a => a.Name == model.First().ArticleName)
                .Include(a => a.ArticleColors)
                .FirstAsync();

            foreach (var item in model)
            {
                article.ArticleColors.Add(new ArticleColor()
                {
                    Article = article,
                    ColorId = item.ColorId,
                    ColorModelId = item.ColorModelId,
                    ColorQuantity = item.ColorQuantity
                });
            };

            await repo.SaveChangesAsync();
        }

        public async Task<AddArticleViewModel> FillAddModelWithDataAsync(AddArticleViewModel model)
        {
            var client = await repo.GetByIdAsync<Client>(model.ClientId);
                       
            if (client == null)
            {
                throw new Exception();
            }

            var materials = await repo.AllReadonly<Material>()
                .Select(m => new MaterialSelectViewModel()
                {
                    Id = m.Id,
                    Type = m.Type
                })
                .ToListAsync();

            var colorModels = await repo.AllReadonly<ColorModel>()
                .Select(cm => new ColorModelSelectViewModel()
                {
                    Id = cm.Id,
                    Name = cm.Name
                })                
                .ToListAsync();

            model.ClientName = client.Name; 
            model.Materials = materials;
            model.ColorModels = colorModels;

            return model;
        }
    }
}
