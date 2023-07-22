namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Constants;
    using Models.Article;
    using Models.ColorModel;
    using Models.Material;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    /// <summary>
    /// Article service
    /// </summary>
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

        /// <summary>
        /// Get all articles or all articles of certain client by client id
        /// </summary>
        /// <param name="id">client id (nullable)</param>
        /// <returns>Enumeration of All article view model</returns>
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
                    FileName = a.ImageName,
                    Material = a.MaterialColorModel.Material.Type,
                    ColorModel = a.MaterialColorModel.ColorModel.Name
                })
                .ToListAsync();

            return models;
        }

        /// <summary>
        /// Creates new article
        /// </summary>
        /// <param name="model">Article view model</param>
        public async Task CreateAsync(ArticleViewModel model)
        {
            var clientArticlesCount = await repo.AllReadonly<Client>(c => c.Id == model.ClientId)
                .Select(c => c.Articles.Count)
                .FirstAsync();

            var article = new Article()
            {
                Name = model.Name,
                ClientId = model.ClientId,
                MaterialId = model.MaterialId,
                ColorModelId = model.ColorModelId,
                Length = model.Length,
                ImageName = model.DesignFile!.FileName,
                ArticleNumber = $"{model.ClientId}.{clientArticlesCount++}"
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

        /// <summary>
        /// Fill data for select material and color model view model
        /// </summary>
        /// <param name="model">Choose article material and color model view model</param>
        /// <returns>Choose article material and color model view model</returns>
        /// <exception cref="Exception">Thrown when clinet is null or client isActive is false</exception>
        public async Task<ChooseArticleMaterialAndColorsViewModel> FillSelectModelWithDataAsync(ChooseArticleMaterialAndColorsViewModel model)
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

        /// <summary>
        /// Check existence of article by id and is it active
        /// </summary>
        /// <param name="id">article id</param>
        /// <returns>Boolean</returns>
        public async Task<bool> ExistByIdAsync(Guid? id)
        {
            var article = await repo.GetByIdAsync<Article>(id);

            return article != null && article.IsActive;
        }

        /// <summary>
        /// Get article by id and whether it is active
        /// </summary>
        /// <param name="id">Guid article id</param>
        /// <returns>Article view model</returns>
        /// <exception cref="ArgumentNullException">Thrown when Article is null</exception>
        public async Task<ArticleViewModel> GetByIdAsync(Guid id)
        {
            var article = await repo
                .AllReadonly<Article>(a => a.Id == id && a.IsActive)
                .Select(a => new ArticleViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ClientId = a.ClientId,
                    DesignName = a.ImageName,
                    ClientName = a.Client.Name,
                    ColorModelId = a.ColorModelId,
                    MaterialId = a.MaterialId,
                    MaterialName = a.MaterialColorModel.Material.Type,
                    MeasureUnit = a.MaterialColorModel.Material.MeasureUnit,
                    Length = a.Length,
                    Colors = a.ArticleColors
                        .Select(ac => new AddArticleColorVeiwModel()
                        {
                            ColorName = ac.Color.Type,
                            ColorId = ac.Color.Id,
                            ColorQuantity = ac.ColorQuantity
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (article == null)
            {
                throw new ArgumentNullException();
            }

            return article;
        }

        /// <summary>
        /// Edit existing article
        /// </summary>
        /// <param name="model">Article view model with data changes</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">thrown when Article is null or IsActive is false, or ClientId is different</exception>
        public async Task EditAsync(ArticleViewModel model)
        {
            var article = await repo.GetByIdAsync<Article>(model.Id);

            if (article == null ||
                article.IsActive == false ||
                article.ClientId != model.ClientId)
            {
                throw new ArgumentNullException();
            }

            var articleColors = repo.All<ArticleColor>(ac => ac.ArticleId == article.Id);

            if (article.ColorModelId == model.ColorModelId &&
                article.MaterialId == model.MaterialId)
            {
                foreach (var color in articleColors)
                {
                    color.ColorQuantity = model.Colors.Where(c => c.ColorId == color.ColorId).First().ColorQuantity;
                }
            }
            else
            {
                article.ArticleColors.Clear();



                repo.DeleteRange(articleColors);

                foreach (var color in model.Colors)
                {
                    article.ArticleColors.Add(new ArticleColor()
                    {
                        Article = article,
                        ColorId = color.ColorId,
                        ColorQuantity = color.ColorQuantity
                    });
                }

                article.MaterialId = model.MaterialId;
                article.ColorModelId = model.ColorModelId;
            }

            article.Name = model.Name;
            article.Length = model.Length;

            if (model.DesignFile != null && model.DesignFile.Length > 0)
            {
                await fileService.SaveFileAsync(article.Id, model.DesignFile.FileName, model.DesignFile);

                article.ImageName = model.DesignFile.FileName;
            }

            await repo.SaveChangesAsync();
        }
    }
}
