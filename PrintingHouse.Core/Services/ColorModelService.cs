namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Article;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    public class ColorModelService : IColorModelService
    {
        private readonly IRepository repo;

        public ColorModelService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<ICollection<AddArticleColorVeiwModel>?> GetColorModelColorsAsync(int ColorModelId)
        {
            var colors =  await repo.AllReadonly<ColorModel>(cm => cm.Id == ColorModelId)
                .Select (cm => cm.Colors
                    .Select(c => new AddArticleColorVeiwModel() 
                    {
                        ColorId = c.Id,
                        ColorModelId = ColorModelId,
                        ColorName = c.Type
                    })
                    .ToList())
                .FirstOrDefaultAsync();

            if (colors == null)
            {
                throw new Exception();
            }

            return colors;
        }
    }
}
