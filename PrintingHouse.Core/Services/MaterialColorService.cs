namespace PrintingHouse.Core.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    /// <summary>
    /// Material - Color model service
    /// </summary>
    public class MaterialColorService : IMaterialColorService
    {
        private readonly IRepository repo;

        public MaterialColorService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Whether exist MaterialColorModel with given material id and color model Id
        /// </summary>
        /// <param name="materialId">Material id</param>
        /// <param name="colorId">ColorModel id</param>
        /// <returns>Boolean</returns>
        public async Task<bool> ExistByIds(int? materialId, int? colorId)
        {
            var entity = await repo
                .AllReadonly<MaterialColorModel>(mc => mc.MaterialId == materialId && 
                                                       mc.ColorModelId == colorId)
                .FirstOrDefaultAsync();

            return entity != null;
        }
    }
}
