namespace PrintingHouse.Core.Contracts
{
    public interface IMaterialService
    {
        Task<bool> ExistByIdAsync(int materialId);
    }
}
