namespace PrintingHouse.Core.Services.Contracts
{
    using AdminModels.Employee;
    using PrintingHouse.Core.AdminModels.ApplicationUser;

    /// <summary>
    /// Employee service interface for IoC
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="model">Add employee view model with form data</param>
        Task AddAsync(AddEmployeeViewModel model);        

        /// <summary>
        /// Gets all active employees
        /// </summary>
        /// <returns>Enumeration with Employee view model</returns>
        Task<IEnumerable<AllEmployeeViewModel>> GetAllAsync();

        /// <summary>
        /// Get active employee by id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>Edit employee view model or null</returns>
        Task<EditEmployeeViewModel?> GetByIdAsync(int id);

        /// <summary>
        /// Change working position of na employee
        /// </summary>
        /// <param name="model">Edit employee view model</param>
        Task ChnagePositionAsync(EditEmployeeViewModel model);

        /// <summary>
        /// Soft delete employee
        /// </summary>
        /// <param name="id">employee identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get employee Id by application user id. May Throw exception from FirstAsync() method
        /// </summary>
        /// <param name="userId">user id (guid)</param>
        /// <returns>employee id</returns>
        Task<int> GetIdByUserIdAsync(Guid userId);

        /// <summary>
        /// Get all registered application users who are not employees yet
        /// </summary>
        /// <returns>Enumeration of All user view model</returns>
        Task<IEnumerable<AllUsersViewModel>> GetAllNewEmployees();
    }
}
