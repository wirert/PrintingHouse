﻿namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Color;

    public interface IColorService
    {
        /// <summary>
        /// Get all colors
        /// </summary>
        /// <returns>Enumeration of Color view model</returns>
        Task<IEnumerable<ColorViewModel>> GetAllAsync();

        /// <summary>
        /// Get all colors of color model
        /// </summary>
        /// <param name="colorModelId"></param>
        /// <returns>Enumeration of Color view model</returns>
        Task<IEnumerable<ColorViewModel>> GetAllByColorModelIdAsync(int colorModelId);

        /// <summary>
        /// Add color quantity to db
        /// </summary>
        /// <param name="id">color id</param>
        /// <param name="quantity">quantity</param>
        /// <returns>color name</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<string> AddToStoreHouseAsync(int id, int quantity);
    }
}
