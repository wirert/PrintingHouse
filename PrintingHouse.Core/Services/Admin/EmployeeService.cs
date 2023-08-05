namespace PrintingHouse.Core.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using AdminModels.ApplicationUser;
    using AdminModels.Employee;
    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Infrastructure.Data.Entities.Account;
    using System.Text;
    using PrintingHouse.Core.Exceptions;

    /// <summary>
    /// Employee service
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IPositionService positionService;

        public EmployeeService(
                    IRepository _repo,
                    UserManager<ApplicationUser> _userManager,
                    RoleManager<IdentityRole<Guid>> _roleManager,
                    IPositionService _positionService)
        {
            repo = _repo;
            userManager = _userManager;
            roleManager = _roleManager;
            positionService = _positionService;
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="model">Add employee view model with form data</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task AddAsync(AddEmployeeViewModel model)
        {
            ApplicationUser user = await CheckEmployeeParametersAndReturnAppUserAsync(model.PositionId, model.Role, model.ApplicationUserId);

            await AddUserToRoleAsync(user, model.Role);

            var employee = new Employee()
            {
                ApplicationUserId = model.ApplicationUserId,
                PositionId = model.PositionId
            };

            await repo.AddAsync(employee);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Soft delete employee and user
        /// </summary>
        /// <param name="id">employee identifier</param>
        /// <param name="currentUserId">Current ApplicatonUser Id</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="EmployeeSelfChangeException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task DeleteAsync(int id, Guid currentUserId)
        {
            var employee = await repo.GetByIdAsync<Employee>(id);

            if (employee == null)
            {
                throw new ArgumentException("Employee id is altered");
            }

            if (employee.ApplicationUserId == currentUserId)
            {
                throw new EmployeeSelfChangeException("You can't remove your own access level!");
            }

            var employeeUser = await userManager.FindByIdAsync(employee.ApplicationUserId.ToString());
            var userRoles = await userManager.GetRolesAsync(employeeUser!);
            var removeRolesResult = await userManager.RemoveFromRolesAsync(employeeUser!, userRoles);

            if (!removeRolesResult.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in removeRolesResult.Errors)
                {
                    sb.AppendLine(error.Code + " " + error.Description);
                }

                throw new Exception(sb.ToString());                
            }

            employeeUser.FirstName = null;
            employeeUser.LastName = null;
            employeeUser.PhoneNumber = null;
            employeeUser.IsActive = false;

            var result = await userManager.SetEmailAsync(employeeUser, null);

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Code + " " + error.Description);
                }

                throw new Exception(sb.ToString());
            }
            
            employee!.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Change working position of an employee
        /// </summary>
        /// <param name="model">Edit employee view model</param>
        /// <param name="currentUserId"></param>
        /// <exception cref="EmployeeSelfChangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task EditAsync(EditEmployeeViewModel model, Guid currentUserId)
        {
            var user = await CheckEmployeeParametersAndReturnAppUserAsync(model.PositionId, model.Role, model.ApplicationUserId);

            if (user != null && user.Id == currentUserId)
            {
               throw new EmployeeSelfChangeException("You can't change your own position!");
            }

            var userRoles = await userManager.GetRolesAsync(user!);

            if (userRoles.Any())
            {
                var removeRolesResult = await userManager.RemoveFromRolesAsync(user!, userRoles);

                if (!removeRolesResult.Succeeded)
                {
                    var sb = new StringBuilder();
                    foreach (var error in removeRolesResult.Errors)
                    {
                        sb.AppendLine(error.Code + " " + error.Description);
                    }

                    throw new Exception(sb.ToString());
                }
            }

            await AddUserToRoleAsync(user!, model.Role);

            var employee = await repo.GetByIdAsync<Employee>(model.Id) 
                ?? throw new ArgumentException($"Employee {model.Id} not exist");
            
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
               
        private async Task<ApplicationUser> CheckEmployeeParametersAndReturnAppUserAsync(int positionId, string roleName, Guid userId)
        {
            var positions = await positionService.GetAllAsync();

            if (positions.All(p => p.Id != positionId))
            {
                throw new ArgumentException("Position is altered");
            }

            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                throw new ArgumentException("Role is altered");
            }

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null || user.IsActive == false)
            {
                throw new ArgumentException("User id is altered");
            }

            return user;
        }

        private async Task AddUserToRoleAsync(ApplicationUser user, string role)
        {
            var result = await userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Code + " " + error.Description);
                }

                throw new Exception(sb.ToString());
            }
        }

    }
}
