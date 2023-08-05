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

    /// <summary>
    /// Color model service
    /// </summary>
    public class ColorModelService : IColorModelService
    {
        private readonly IRepository repo;

        public ColorModelService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Get Color models for particular Material
        /// </summary>
        /// <param name="materialId">material identifier</param>
        /// <returns>Collection of Color model view model</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ICollection<ColorModelSelectViewModel>> GetColorModelByMaterialIdAsync(string materialId)
        {
            bool isIntMaterialId = int.TryParse(materialId, out int matId);
            List<ColorModelSelectViewModel> colorModelList = new List<ColorModelSelectViewModel>();

            if (isIntMaterialId)
            {
                colorModelList = await repo.AllReadonly<MaterialColorModel>(mc => mc.MaterialId == matId)
                    .Select(mc => mc.ColorModel)
                    .Select(cm => new ColorModelSelectViewModel()
                    {
                        Id = cm.Id,
                        Name = cm.Name,
                    })
                    .ToListAsync();
            }
            else
            {
                throw new ArgumentException("Not a number", nameof(materialId));
            }

            return colorModelList;
        }

        /// <summary>
        /// Gets the colors list of particular color model by id
        /// </summary>
        /// <param name="colorModelId">Color model identifier</param>
        /// <returns>List of color View model</returns>
        public async Task<IList<AddArticleColorVeiwModel>> GetColorModelColorsAsync(int colorModelId)
        {
            var colors =  await repo.AllReadonly<ColorModel>(cm => cm.Id == colorModelId)
                .Select (cm => cm.Colors
                    .Select(c => new AddArticleColorVeiwModel() 
                    {
                        ColorId = c.Id,
                        ColorName = c.Type
                    })
                    .ToList())
                .FirstOrDefaultAsync();

            return colors!;
        }
    }
}
