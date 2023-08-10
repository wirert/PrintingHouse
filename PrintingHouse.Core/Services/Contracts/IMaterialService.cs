namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Material;

    /// <summary>
    /// Material service interface for IoC
    /// </summary>
    public interface IMaterialService
    {
        /// <summary>
        /// Get material name by identifier or null if not exist
        /// </summary>
        /// <param name="materialId">material identifier</param>
        /// <returns>material name</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<MaterialSelectViewModel> GetMaterialByIdAsync(int? materialId);

        /// <summary>
        /// Gets all active materials
        /// </summary>
        /// <returns>Enumeration of material view model</returns>
        Task<IEnumerable<MaterialViewModel>> GetAllMaterialsAsync();

        /// <summary>
        /// Adds materials in db
        /// </summary>
        /// <param name="id">material id</param>
        /// <param name="quantity">quantity to add</param>
        /// <returns>Material name</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<string> AddToStoreHouseAsync(int id, int quantitiy);
    }
}
