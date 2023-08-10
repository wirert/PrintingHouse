namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Color;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    public class ColorService : IColorService
    {
        private readonly IRepository repo;

        public ColorService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Add color quantity to db
        /// </summary>
        /// <param name="id">color id</param>
        /// <param name="quantity">quantity</param>
        /// <returns>color name</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddToStoreHouseAsync(int id, int quantity)
        {
            if (quantity < 0 || quantity > 100000)
            {
                throw new ArgumentException("Quantity must be between 0 and 100000");
            }
            var color = await repo.GetByIdAsync<Color>(id);

            if (color == null)
            {
                throw new ArgumentException("Incorrect color id");
            }

            if (color.InStock > int.MaxValue - quantity)
            {
                throw new ArgumentException("Too much quantity in stock");
            }

            color.InStock += quantity;
            await repo.SaveChangesAsync();

            return color.Type;
        }

        /// <summary>
        /// Get all colors
        /// </summary>
        /// <returns>Enumeration of Color view model</returns>
        public async Task<IEnumerable<ColorViewModel>> GetAllAsync()
        {   
            return await repo.AllReadonly<Color>()
                .Select(c => new ColorViewModel()
                {
                    Id = c.Id,
                    Name = c.Type,
                    InStock = c.InStock,
                    Price = c.Price,
                    ColorModel = c.ColorModel.Name
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get all colors of color model
        /// </summary>
        /// <param name="colorModelId"></param>
        /// <returns>Enumeration of Color view model</returns>
        public async Task<IEnumerable<ColorViewModel>> GetAllByColorModelIdAsync(int colorModelId)
        {
            return await repo.AllReadonly<Color>(c => c.ColorModelId == colorModelId)
                .Select(c => new ColorViewModel()
                {
                    Id = c.Id,
                    Name = c.Type,
                    InStock = c.InStock,
                    Price = c.Price,
                    ColorModel = c.ColorModel.Name
                })
                .ToListAsync();
        }
    }
}
