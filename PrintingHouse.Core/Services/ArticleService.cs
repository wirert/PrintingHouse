namespace PrintingHouse.Core.Services
{
    using System.Threading.Tasks;

    using Contracts;
    using Models.Article;
    using Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using PrintingHouse.Core.Models.Material;
    using PrintingHouse.Core.Models.ColorModel;
    using Microsoft.AspNetCore.Mvc;

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

        public async Task<AddArticleViewModel> FillAddModelWithData(AddArticleViewModel model)
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
