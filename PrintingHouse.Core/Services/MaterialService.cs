namespace PrintingHouse.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using PrintingHouse.Core.Models.Material;
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Material service
    /// </summary>
    public class MaterialService : IMaterialService
    {
        private readonly IRepository repo;

        public MaterialService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Get material name by identifier or null if not exist
        /// </summary>
        /// <param name="materialId">material identifier</param>
        /// <returns>material name</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<MaterialSelectViewModel> GetMaterialByIdAsync(int? materialId)
        {
            var material = await repo.AllReadonly<Material>(m => m.Id == materialId && m.IsActive)
                .Select(m => new MaterialSelectViewModel()
                {
                    Id = materialId ?? -1,
                    Type = m.Type,
                    MeasureUnit = m.MeasureUnit
                })
                .FirstOrDefaultAsync();
            if (material == null || materialId == null)
            {
                throw new ArgumentException("Material Id is not valid");
            }

            return material;
        }
    }
}
