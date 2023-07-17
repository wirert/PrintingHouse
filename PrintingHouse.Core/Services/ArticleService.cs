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
    using System.Linq.Expressions;

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

        public async Task<IEnumerable<AllArticleViewModel>> GetAllAsync(int? id)
        {
            Expression<Func<Article, bool>> searchTerms = a => a.IsActive;

            if (id.HasValue)
            {
                searchTerms = (a) => a.IsActive && a.ClientId == id;
            }

            var models = await repo.AllReadonly(searchTerms)
                .Select(a => new AllArticleViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ClientId = a.ClientId,
                    ClientName = a.Client.Name,
                    ImageName = a.ImageName,
                    ColorModel = a.MaterialColorModel.ColorModel.Name
                })
                .ToListAsync();

            return models;
        }

        public async Task CreateAsync(CreateArticleViewModel model)
        {
            var article = new Article()
            {
                Name = model.Name,
                ClientId = model.ClientId,
                MaterialId = model.MaterialId,
                ColorModelId = model.ColorModelId,
                MaterialQuantity = model.MaterialQuantity,
                ImageName = model.DesignFile.FileName
            };

            foreach (var color in model.Colors)
            {
                article.ArticleColors.Add(new ArticleColor()
                {
                    Article = article,
                    ColorId = color.ColorId,
                    ColorQuantity = color.ColorQuantity
                });
            }

            await fileService.SaveFileAsync(article.Id, model.DesignFile.FileName, model.DesignFile);

            await repo.AddAsync(article);
            await repo.SaveChangesAsync();
        }

        public async Task<ChooseArticleMaterialAndColorsViewModel> FillAddModelWithDataAsync(ChooseArticleMaterialAndColorsViewModel model)
        {
            var client = await repo.GetByIdAsync<Client>(model.ClientId);

            if (client == null || client.IsActive == false)
            {
                throw new Exception();
            }
                
            model.Materials = await repo.AllReadonly<Material>(m => m.IsActive)
                .Select(m => new MaterialSelectViewModel()
                {
                    Id = m.Id,
                    Type = m.Type
                })
                .ToListAsync();

            model.Materials.Add(new MaterialSelectViewModel()
            {
                Id = 0,
                Type = "--Select Material--"
            });

            model.ColorModels.Add(new ColorModelSelectViewModel()
            {
                Id = 0,
                Name = "--Select Color model--"
            });

            model.ClientName = client.Name;

            return model;
        }


    }
}
