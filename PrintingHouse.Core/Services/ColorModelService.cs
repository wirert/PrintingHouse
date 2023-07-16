namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Article;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using PrintingHouse.Core.Models.ColorModel;

    public class ColorModelService : IColorModelService
    {
        private readonly IRepository repo;

        public ColorModelService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> ExistByIdAsync(int colorModelId)
        {
            var colorModel = await repo.GetByIdAsync<ColorModel>(colorModelId);

            return colorModel != null;
        }

        public async Task<ICollection<ColorModelSelectViewModel>> GetColorModelByMaterialIdAsync(string materialId)
        {
            int matId;
            List<ColorModelSelectViewModel> colorModelList = new List<ColorModelSelectViewModel>();
            if (!string.IsNullOrEmpty(materialId))
            {
                matId = Convert.ToInt32(materialId);
                colorModelList = await repo.AllReadonly<MaterialColorModel>(mc => mc.MaterialId == matId)
                    .Select(mc => mc.ColorModel)
                    .Select(cm => new ColorModelSelectViewModel()
                    {
                        Id = cm.Id,
                        Name = cm.Name,
                    })
                    .ToListAsync();    
            }

            return colorModelList;
        }

        public async Task<IList<AddArticleColorVeiwModel>> GetColorModelColorsAsync(int ColorModelId)
        {
            var colors =  await repo.AllReadonly<ColorModel>(cm => cm.Id == ColorModelId)
                .Select (cm => cm.Colors
                    .Select(c => new AddArticleColorVeiwModel() 
                    {
                        ColorId = c.Id,
                        ColorName = c.Type,
                        ColorModelId = ColorModelId
                    })
                    .ToList())
                .FirstOrDefaultAsync();

            if (colors == null)
            {
                throw new ArgumentException();
            }

            return colors;
        }
    }
}
