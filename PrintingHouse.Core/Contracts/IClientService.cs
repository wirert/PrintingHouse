namespace PrintingHouse.Core.Contracts
{
    using PrintingHouse.Core.Models.Client;

    public interface IClientService
    {
        Task AddNewAsync(AddClientViewModel model);

        Task<bool> ExistByName(string name);

        Task<IEnumerable<AllClientViewModel>> GetAllAsync();

    }
}
