namespace PrintingHouse.Core.Services.Contracts
{
    /// <summary>
    /// Material - Color model service interface for IoC
    /// </summary>
    public interface IMaterialColorService
    {
        /// <summary>
        /// Whether exist MaterialColorModel with given material id and color model Id
        /// </summary>
        /// <param name="materialId">Material id</param>
        /// <param name="colorId">ColorModel id</param>
        /// <returns>Boolean</returns>
        Task<bool> ExistByIds(int materialId, int colorId);
    }
}
