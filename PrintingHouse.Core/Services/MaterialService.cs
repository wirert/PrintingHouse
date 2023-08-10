namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Material;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

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
        /// Adds materials in db
        /// </summary>
        /// <param name="id">material id</param>
        /// <param name="quantity">quantity to add</param>
        /// <returns>Material name</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddToStoreHouseAsync(int id, int quantity)
        {
            if (quantity < 0 || quantity > 100000)
            {
                throw new ArgumentException("Quantity must be between 0 and 100000");
            }
            var material = await repo.GetByIdAsync<Material>(id);

            if (material == null || material.IsActive == false)
            {
                throw new ArgumentException("Incorrect Material id");
            }

            if (material.InStock > int.MaxValue - quantity)
            {
                throw new ArgumentException("Too much materials");
            }

            material.InStock += quantity;
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
