namespace PrintingHouse.Core.Contracts
{
    using PrintingHouse.Core.AdminModels.Position;

    public interface IPositionService
    {
        Task<IEnumerable<AllPositionViewModel>> GetAllAsync();

        Task AddNewAsync(AddPositionViewModel viewModel);
    }
}
