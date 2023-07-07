namespace PrintingHouse.Core.Contracts
{
    using AdminModels.Position;

    public interface IPositionService
    {
        Task<IEnumerable<PositionViewModel>> GetAllAsync();

        Task AddNewAsync(AddPositionViewModel viewModel);

        Task DeleteAsync (int positionId);
    }
}
