namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Article;
    using PrintingHouse.Core.Models.ColorModel;

    /// <summary>
    /// Color model service interface for IoC
    /// </summary>
    public interface IColorModelService
    {
        /// <summary>
        /// Gets the colors list of particular color model by id
        /// </summary>
        /// <param name="colorModelId">Color model identifier</param>
        /// <returns>List of color View model</returns>
        Task<IList<AddArticleColorVeiwModel>> GetColorModelColorsAsync(int colorModelId);

        /// <summary>
        /// Get Color models for particular Material
        /// </summary>
        /// <param name="materialId">material identifier</param>
        /// <returns>Collection of Color model view model</returns>
        Task<ICollection<ColorModelSelectViewModel>> GetColorModelByMaterialIdAsync(string materialId);        
    }
}
