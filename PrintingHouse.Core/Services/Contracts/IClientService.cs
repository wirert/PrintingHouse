namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Client;

    public interface IClientService
    {
        Task AddNewAsync(AddClientViewModel model);

        Task<string?> GetNameByIdAsync(int id);

        Task<bool> ExistByName(string name);

        Task<IEnumerable<AllClientViewModel>> GetAllAsync();

    }
}
