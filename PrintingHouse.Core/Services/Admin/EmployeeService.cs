namespace PrintingHouse.Core.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using AdminModels.Employee;
    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Infrastructure.Data.Entities.Account;
    using PrintingHouse.Core.AdminModels.ApplicationUser;

    /// <summary>
    /// Employee service
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repo;
        private readonly UserManager<ApplicationUser> userManager;

        public EmployeeService(
                    IRepository _repo,
                    UserManager<ApplicationUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="model">Add employee view model with form data</param>
        public async Task AddAsync(AddEmployeeViewModel model)
        {
            var employee = new Employee()
            {
                ApplicationUserId = model.ApplicationUserId,
                PositionId = model.PositionId
            };

            await repo.AddAsync(employee);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Soft delete employee
        /// </summary>
        /// <param name="id">employee identifier</param>
        public async Task DeleteAsync(int id)
        {
            var employee = await repo.GetByIdAsync<Employee>(id);

            employee.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Change working position of na employee
        /// </summary>
        /// <param name="model">Edit employee view model</param>
        public async Task ChnagePositionAsync(EditEmployeeViewModel model)
        {
            var employee = await repo.GetByIdAsync<Employee>(model.Id);

            if (employee.PositionId == model.PositionId)
            {
                return;
            }

            employee.PositionId = model.PositionId;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all active employees
        /// </summary>
        /// <returns>Enumeration with Employee view model</returns>
        public async Task<IEnumerable<AllEmployeeViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Employee>(e => e.IsActive)
                .Select(e => new AllEmployeeViewModel
                {
                    Id = e.ApplicationUserId.ToString(),
                    FullName = $"{e.ApplicationUser.FirstName} {e.ApplicationUser.LastName}",
                    PhoneNumber = e.ApplicationUser.PhoneNumber,
                    EmployeeNumber = e.Id,
                    Position = e.Position.Name
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get active employee by id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>Edit employee view model or null</returns>
        public async Task<EditEmployeeViewModel?> GetByIdAsync(int id)
        {
            var employee = await repo.GetByIdAsync<Employee>(id);

            if (employee == null || employee.IsActive == false)
            {
                return null;
            }

            return new EditEmployeeViewModel()
            {
                Id = employee.Id,
                ApplicationUserId = employee.ApplicationUserId,
                PositionId = employee.PositionId
            };

        }

        /// <summary>
        /// Get employee Id by application user id. May Throw exception from FirstAsync() method
        /// </summary>
        /// <param name="userId">user id (guid)</param>
        /// <returns>employee id</returns>
        public async Task<int> GetIdByUserIdAsync(Guid userId)
        {
            var employee = await repo
                .All<Employee>(e => e.IsActive && e.ApplicationUserId == userId)
                .FirstAsync();

            return employee.Id;
        }

        /// <summary>
        /// Get all registered application users who are not employees yet
        /// </summary>
        /// <returns>Enumeration of All user view model</returns>
        public async Task<IEnumerable<AllUsersViewModel>> GetAllNewEmployees()
        {
            var employeeUserIds = repo.AllReadonly<Employee>()
                .Select(e => e.ApplicationUserId);

            return await userManager.Users
                    .Where(u => u.IsActive &&
                            employeeUserIds.All(id => id != u.Id))
                    .Select(u => new AllUsersViewModel()
                    {
                        Id = u.Id,
                        FullName = $"{u.FirstName} {u.LastName}"
                    })
                    .ToListAsync();
        }
    }
}
