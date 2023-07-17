namespace PrintingHouse.Core.Services.Contracts
{
    public interface IMaterialService
    {
        Task<string?> GetNameByIdIfExistAsync(int materialId);
    }
}
