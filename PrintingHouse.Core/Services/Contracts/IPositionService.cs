namespace PrintingHouse.Core.Services.Contracts
{
    using AdminModels.Position;

    /// <summary>
    /// Position service interface for IoC
    /// </summary>
    public interface IPositionService
    {
        /// <summary>
        /// Get all active positions available
        /// </summary>
        /// <returns>Enumeration of Position view model</returns>
        Task<IEnumerable<PositionViewModel>> GetAllAsync();

        /// <summary>
        /// Create new position or restore if non active
        /// </summary>
        /// <param name="viewModel">Add position view model</param>
        /// <exception cref="ArgumentException"></exception>
        Task AddNewAsync(AddPositionViewModel viewModel);

        /// <summary>
        /// Soft delete position
        /// </summary>
        /// <param name="positionId">Positon id</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Throw if there are workers on this position</exception>
        /// <exception cref="ArgumentException"></exception>
        Task DeleteAsync(int positionId);
    }
}
