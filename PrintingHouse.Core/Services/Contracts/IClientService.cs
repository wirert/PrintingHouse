namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Client;

    /// <summary>
    /// Client service interface for IoC
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Create new client
        /// </summary>
        /// <param name="model">Add client view model with data from form</param>
        /// <returns></returns>
        Task AddNewAsync(AddClientViewModel model);

        /// <summary>
        /// Whether client exist by given name
        /// </summary>
        /// <param name="name">Client name</param>
        /// <returns>Boolean</returns>
        Task<bool> ExistByName(string name);

        /// <summary>
        /// Gets all active clients
        /// </summary>
        /// <returns>Enumeration of All client view model</returns>
        Task<IEnumerable<AllClientViewModel>> GetAllAsync();

    }
}
