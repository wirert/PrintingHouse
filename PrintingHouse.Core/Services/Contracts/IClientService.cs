namespace PrintingHouse.Core.Services.Contracts
{
    using Exceptions;
    using Models.Client;

    /// <summary>
    /// Client service interface for IoC
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Create new client or restore and update deleted
        /// </summary>
        /// <param name="model">Add client view model with data from form</param>        
        /// <param name="userId">current user id</param>
        /// <returns></returns>
        /// <exception cref="ClientNameExistsException"></exception>
        Task AddNewAsync(AddClientViewModel model, Guid userId);

        /// <summary>
        /// Check whether Client with given id and name exists
        /// </summary>
        /// <param name="id">client id</param>
        /// <param name="name">client name</param>
        /// <returns>Boolean</returns>
        Task<bool> ExistsByIdAndNameAsync(Guid id, string name);

        /// <summary>
        /// Gets all active clients
        /// </summary>
        /// <returns>Enumeration of All client view model</returns>
        Task<IEnumerable<AllClientViewModel>> GetAllAsync();

        /// <summary>
        /// Soft delete Client with his articles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DeleteClientException"></exception>
        Task DeleteAsync(Guid id);

    }
}
