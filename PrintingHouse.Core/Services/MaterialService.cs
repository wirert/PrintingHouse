namespace PrintingHouse.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using PrintingHouse.Core.Models.Material;
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities;
    using System.Collections.Generic;
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

        public async Task<string> AddToStoreHouseAsync(int id, int quantitiy)
        {
            if (quantitiy < 0)
            {
                throw new ArgumentException("Quantity must be between 0 and 100000");
            }
            var material = await repo.GetByIdAsync<Material>(id);

            if (material == null)
            {
                throw new ArgumentException("Incorrect Material id");
            }

            if (material.InStock > int.MaxValue - quantitiy)
            {
                throw new ArgumentException("Too much materials");
            }

            material.InStock += quantitiy;
            await repo.SaveChangesAsync();
           
            return material.Type;
        }

        /// <summary>
        /// Gets all active materials
        /// </summary>
        /// <returns>Enumeration of material view model</returns>
        public async Task<IEnumerable<MaterialViewModel>> GetAllMaterialsAsync()
        {
            var materials = await repo
                .AllReadonly<Material>(m => m.IsActive)
                .Select(m => new MaterialViewModel()
                {
                    Id = m.Id,
                    Type = m.Type,
                    InStock = m.InStock,
                    Lenght = m.Lenght,
                    Width = m.Width,
                    MeasureUnit = m.MeasureUnit,
                    Price = m.Price                    
                })
                .ToListAsync();

            return materials;
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
