namespace PrintingHouse.Core.Services.Contracts
{
    using AdminModels.Employee;
    using AdminModels.ApplicationUser;
    using Exceptions;

    /// <summary>
    /// Employee service interface for IoC
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="model">Add employee view model with form data</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
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
        /// Change working position of an employee
        /// </summary>
        /// <param name="model">Edit employee view model</param>
        /// <param name="currentUserId"></param>
        /// <exception cref="EmployeeSelfChangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        Task EditAsync(EditEmployeeViewModel model, Guid currentUserId);

        /// <summary>
        /// Soft delete employee and user
        /// </summary>
        /// <param name="id">employee identifier</param>
        /// <param name="currentUserId">Current ApplicatonUser Id</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="EmployeeSelfChangeException"></exception>
        /// <exception cref="Exception"></exception>
        Task DeleteAsync(int id, Guid currentUserId);       

        /// <summary>
        /// Get all registered application users who are not employees yet
        /// </summary>
        /// <returns>Enumeration of All user view model</returns>
        Task<IEnumerable<AllUsersViewModel>> GetAllNewEmployees();
    }
}
