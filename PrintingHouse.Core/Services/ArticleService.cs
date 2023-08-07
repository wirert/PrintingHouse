namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Constants;
    using Models.Article;
    using Models.ColorModel;
    using Models.Material;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Infrastructure.Data.Entities.Enums;

    /// <summary>
    /// Article service
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly IRepository repo;
        private readonly IFileService fileService;
        private readonly IMaterialColorService materialColorService;
        private readonly IMaterialService materialService;
        private readonly IClientService clientService;
        private readonly IColorModelService colorModelService;

        public ArticleService(
                        IRepository _repo,
                        IFileService _fileService,
                        IMaterialColorService _materialColorService,
                        IMaterialService _materialService,
                        IClientService _clientService,
                        IColorModelService _colorModelService
                        )
        {
            repo = _repo;
            fileService = _fileService;
            materialColorService = _materialColorService;
            materialService = _materialService;
            clientService = _clientService;
            colorModelService = _colorModelService;            
        }

        /// <summary>
        /// Get all articles or all articles of certain client by client id
        /// </summary>
        /// <param name="clientId">client id (nullable)</param>
        /// <returns>Enumeration of All article view model</returns>
        public async Task<IEnumerable<AllArticleViewModel>> GetAllAsync(Guid? clientId)
        {
            Expression<Func<Article, bool>> searchTerms = a => a.IsActive;

            if (clientId.HasValue)
            {
                searchTerms = (a) => a.IsActive && a.ClientId == clientId;
            }

            var models = await repo.AllReadonly(searchTerms)
                .Select(a => new AllArticleViewModel()
                {
                    Id = a.Id,
                    ArticleNumber = a.ArticleNumber,
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
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateAsync(ArticleViewModel model)
        {
            var client = await repo.AllReadonly<Client>(c => c.Id == model.ClientId)
                .Select(c => new
                {
                    ArticlesCount = c.Articles.Count(a => a.IsActive),
                    c.ClientNumber,
                    c.Name
                })
                .FirstOrDefaultAsync();

            if (client == null || model.ClientName != client.Name)
            {
                throw new ArgumentException("Client name or id is altered");
            }

            if (!await materialColorService.ExistByIds(model.MaterialId, model.ColorModelId))
            {
                throw new ArgumentException("Material Id or ColorModel Id is altered");
            }

            var article = new Article()
            {
                Name = WebUtility.HtmlEncode(model.Name),
                ClientId = model.ClientId,
                MaterialId = model.MaterialId,
                ColorModelId = model.ColorModelId,
                Length = model.Length,
                ArticleNumber = $"{client.ClientNumber}.{client.ArticlesCount + 1}"
            };

            var rnd = new Random();
            var extention = model.DesignFile!.FileName.Split('.', StringSplitOptions.RemoveEmptyEntries).Last();
            article.ImageName = $"{article.ArticleNumber}_{rnd.Next(10000)}.{extention}";

            foreach (var color in model.Colors)
            {
                article.ArticleColors.Add(new ArticleColor()
                {
                    Article = article,
                    ColorId = color.ColorId,
                    ColorQuantity = color.ColorQuantity
                });
            }

            await fileService.SaveFileAsync(article.Id, article.ImageName, model.DesignFile!);

            await repo.AddAsync(article);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Fill data for select material and color model view model
        /// </summary>        
        /// <param name="clientId"></param>
        /// <param name="articleId"></param>
        /// <returns>Choose article material and color model view model</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ChooseArticleMaterialAndColorsViewModel> GetSelectVeiwModelWithDataAsync(Guid clientId, Guid? articleId)
        {
            if (articleId != null &&
                await ExistByIdAsync(articleId) == false)
            {
                throw new ArgumentException("Article id is altered.");
            }

            var client = await repo.GetByIdAsync<Client>(clientId);

            if (client == null || client.IsActive == false)
            {
                throw new ArgumentException("Client id is altered");
            }

            var model = new ChooseArticleMaterialAndColorsViewModel()
            {
                ArticleId = articleId,
                ClientId = clientId,
                ClientName = client.Name                
            };

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
        /// Edit existing article
        /// </summary>
        /// <param name="model">Article view model with data changes</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task EditAsync(ArticleViewModel model)
        {     
            var article = await repo.GetByIdAsync<Article>(model.Id);

            if (article == null ||
                article.ClientId != model.ClientId ||
               !await materialColorService.ExistByIds(model.MaterialId, model.ColorModelId) ||
               !await clientService.ExistsByIdAndNameAsync(model.ClientId, model.ClientName)
               )
            {           
                throw new ArgumentException("Arguments are altered!");
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

            article.Name = WebUtility.HtmlEncode(model.Name);
            article.Length = model.Length;

            if (model.DesignFile != null && model.DesignFile.Length > 0)
            {
                var rnd = new Random();
                var extention = model.DesignFile.FileName.Split('.', StringSplitOptions.RemoveEmptyEntries).Last();
                var newFileName = $"{article.ArticleNumber}_{rnd.Next(10000)}.{extention}";

                await fileService.SaveFileAsync(article.Id, newFileName, model.DesignFile);

                article.ImageName = newFileName;
            }

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Soft delete article
        /// </summary>
        /// <param name="id">Article id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteByIdAsync(Guid id)
        {
            var article = await repo.GetByIdAsync<Article>(id);

            if (article == null)
            {
                throw new ArgumentNullException(nameof(article), "Article id is altered");
            }

            article.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Get design name by article id
        /// </summary>
        /// <param name="id">Article identifier</param>
        /// <returns>Design name</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetFileNameByIdAsync(Guid id)
        {
            var imageName = await repo.AllReadonly<Article>(a => a.Id == id && a.IsActive)
                 .Select(a => a.ImageName)
                 .SingleOrDefaultAsync();
            if (imageName == null)
            {
                throw new ArgumentNullException(nameof(imageName), $"Article with id {id} is not found");
            }

            return imageName;
        }

        /// <summary>
        /// Creates view model for creating a new article and fill it with needed data
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="colorModelId"></param>
        /// <param name="clientId"></param>
        /// <param name="clientName"></param>
        /// <returns>Article view model</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ArticleViewModel> GetCreateViewModelWithData(int materialId, int colorModelId, Guid clientId, string? clientName)
        {
            var material = await materialService.GetMaterialByIdAsync(materialId);

            if (material == null
                || string.IsNullOrWhiteSpace(clientName)
                || !await materialColorService.ExistByIds(materialId, colorModelId)
                || !await clientService.ExistsByIdAndNameAsync(clientId, clientName)
                )
            {
                throw new ArgumentException("Input data is altered.");
            }

            var model = new ArticleViewModel()
            {
                MaterialId = materialId,
                MaterialName = material!.Type,
                MeasureUnit = (MeasureUnit)material.MeasureUnit!,
                ColorModelId = colorModelId,
                ClientId = clientId,
                ClientName = clientName
            };

            if (material.MeasureUnit == MeasureUnit.Piece)
            {
                model.Length = ModelConstants.Article_Piece_Length;
            }

            model.Colors = await colorModelService.GetColorModelColorsAsync(colorModelId);


            return model;
        }

        /// <summary>
        /// Finds an article from db and create view model for update
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="colorModelId"></param>
        /// <param name="articleId"></param>
        /// <returns>Article view model</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ArticleViewModel> GetEditViewModelWithData(int? materialId, int? colorModelId, Guid articleId)
        {
            var model = await repo
                        .AllReadonly<Article>(a => a.Id == articleId && a.IsActive)
                        .Select(a => new ArticleViewModel()
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ClientId = a.ClientId,
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
            if (model == null)
            {
                throw new ArgumentException("Article id is not valid");
            }

            if (materialId != null && colorModelId != null)
            {
                var material = await materialService.GetMaterialByIdAsync(materialId);

                if (!await materialColorService.ExistByIds(materialId, colorModelId))
                {
                    throw new ArgumentException("Input data (materialId or colorModelId) is altered");
                }

                model.MaterialName = material.Type;
                model.MeasureUnit = (MeasureUnit)material.MeasureUnit!;

                if (model.MeasureUnit == MeasureUnit.Piece)
                {
                    model.Length = ModelConstants.Article_Piece_Length;
                }

                model.Colors = await colorModelService.GetColorModelColorsAsync(colorModelId ?? 0);                
            }

            return model;
        }
    }
}
