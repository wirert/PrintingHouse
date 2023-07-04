namespace PrintingHouse.Core.Contracts
{
    using PrintingHouse.Core.Models.Position;

    public interface IPositionService
    {
        Task<IEnumerable<AllPositionViewModel>> GetAllAsync();
    }
}
