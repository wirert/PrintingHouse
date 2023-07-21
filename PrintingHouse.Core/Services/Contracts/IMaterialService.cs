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
        /// <returns>material name or null</returns>
        Task<MaterialSelectViewModel?> GetMaterialByIdAsync(int materialId);
    }
}
