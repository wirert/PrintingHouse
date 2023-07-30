namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Core.AdminModels.Employee;
    using static Core.Constants.MessageConstants;
    using Core.Services.Contracts;
    using Infrastructure.Data.Entities.Account;

    /// <summary>
    /// Employee controller in admin area
    /// </summary>
    public class EmployeeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IEmployeeService employeeService;
        private readonly IPositionService positionService;

        public EmployeeController(
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
            IEmployeeService _employeeService,
            IPositionService _positionService)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            employeeService = _employeeService;
            positionService = _positionService;
        }

        /// <summary>
        /// Add new employee (get)
        /// </summary>
        /// <returns>View with Employee view model</returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var users = await employeeService.GetAllNewEmployees();

                if (users.Count() == 0)
                {
                    TempData[WarningMessage] = "There are no new registered employees.";

                    return RedirectToAction("All");
                }

                var roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();

                var positions = await positionService.GetAllAsync();

                var model = new AddEmployeeViewModel()
                {
                    Users = users,
                    AccessLevels = roles,
                    Positions = positions
                };

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error! Try again later.";

                return RedirectToAction("All", "Employee");
            }
        }

        /// <summary>
        /// Creates new employee with registered application user data
        /// </summary>
        /// <param name="model">Add employee view model with form parameters</param>
        /// <returns>Redirect to All employees action</returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            try
            {
                var positions = await positionService.GetAllAsync();

                if (positions.All(p => p.Id != model.PositionId))
                {
                    ModelState.AddModelError(nameof(model.PositionId), "You should select a position!");
                }

                var user = await userManager.FindByIdAsync(model.ApplicationUserId.ToString());

                if (user == null || user.IsActive == false)
                {
                    ModelState.AddModelError(nameof(model.ApplicationUserId), "You should select a user!");
                }

                var role = await roleManager.FindByNameAsync(model.Role);

                if (role == null)
                {
                    ModelState.AddModelError(nameof(model.Role), "You should select a role!");
                }

                if (!ModelState.IsValid)
                {
                    var users = await employeeService.GetAllNewEmployees();
                    var roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();

                    model.Users = users;
                    model.Positions = positions;
                    model.AccessLevels = roles;

                    return View(model);
                }

                var result = await userManager.AddToRoleAsync(user!, model.Role);

                if (!result.Succeeded)
                {
                    throw new Exception();
                }

                await employeeService.AddAsync(model);

                TempData[SuccessMessage] = "Employee was added successfully!";

                return RedirectToAction("All");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add employee!");

                return RedirectToAction("All", "Employee");
            }
        }

        /// <summary>
        /// Show all active employees
        /// </summary>
        /// <returns>View with Collection of employee view model</returns>
        public async Task<IActionResult> All()
        {
            var model = await employeeService.GetAllAsync();

            return View(model);
        }

        /// <summary>
        /// Edit existing employee data (position and permissions)
        /// </summary>
        /// <param name="id">employee identifier</param>
        /// <returns>View with model</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {            
            try
            {
                var model = await employeeService.GetByIdAsync(id);

                if (model == null)
                {
                    TempData[ErrorMessage] = "Employee with such Id does not exist!";

                    return RedirectToAction("All");
                }

                var user = await userManager.FindByIdAsync(model.ApplicationUserId.ToString());

                var roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();
                var positions = await positionService.GetAllAsync();

                model.FullName = $"{user.FirstName} {user.LastName}";
                model.Positions = positions;
                model.Roles = roles;

                string currentPossition = positions.First(p => p.Id == model.PositionId).Name;

                model.OldPositionName = currentPossition;

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong! Try again.";

                return RedirectToAction("All");
            }
        }

        /// <summary>
        /// Change position and permissions of an employee
        /// </summary>
        /// <param name="model">Edit employee view model</param>
        /// <returns>Redirect to All employees action</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeViewModel model)
        {
            try
            {
                var positions = await positionService.GetAllAsync();

                if (positions.All(p => p.Id != model.PositionId))
                {
                    ModelState.AddModelError(nameof(model.PositionId), "You should select a position!");
                }

                var user = await userManager.FindByIdAsync(model.ApplicationUserId.ToString());

                if (user == null || user.IsActive == false)
                {
                    ModelState.AddModelError(nameof(model.ApplicationUserId), "You should select a user!");
                }

                var role = await roleManager.FindByNameAsync(model.Role);

                if (role == null)
                {
                    ModelState.AddModelError(nameof(model.Role), "You should select a role!");
                }

                if (!ModelState.IsValid)
                {
                    var roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();

                    model.Positions = positions;
                    model.Roles = roles;

                    return View(model);
                }

                var userRoles = await userManager.GetRolesAsync(user!);

                if (userRoles.Any())
                {
                    var removeRolesResult = await userManager.RemoveFromRolesAsync(user!, userRoles);

                    if (!removeRolesResult.Succeeded)
                    {
                        throw new Exception();
                    }
                }

                var addRoleResult = await userManager.AddToRoleAsync(user!, model.Role);

                if (!addRoleResult.Succeeded)
                {
                    throw new Exception();
                }

                await employeeService.ChnagePositionAsync(model);

                TempData[SuccessMessage] = "Position was changed successfully!";

                return RedirectToAction("All", "Employee");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred while trying to change position!";

                return RedirectToAction("All", "Employee");
            }
        }

        /// <summary>
        /// Soft delete for employee with delete of personal data and permissions (roles)
        /// </summary>
        /// <param name="id">employee identifier</param>
        /// <returns>Redirects to All employees action</returns>
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            try
            {
                var employee = await employeeService.GetByIdAsync(id);

                if (employee == null)
                {
                    throw new ArgumentException("There is no such employee!");
                }

                var employeeUser = await userManager.FindByIdAsync(employee.ApplicationUserId.ToString());

                var userRoles = await userManager.GetRolesAsync(employeeUser!);
                var removeRolesResult = await userManager.RemoveFromRolesAsync(employeeUser!, userRoles);

                if (!removeRolesResult.Succeeded)
                {
                    throw new ArgumentException("Problem removing user access level!");
                }


                employeeUser.FirstName = null;
                employeeUser.LastName = null;
                employeeUser.PhoneNumber = null;
                employeeUser.IsActive = false;

                var result = await userManager.SetEmailAsync(employeeUser, null);

                if (!result.Succeeded)
                {
                    throw new ArgumentException($"Failed to delete user from the system. Try again later!");
                }

                await employeeService.DeleteAsync(id);

                TempData[SuccessMessage] = $"Employee was successfully dismissed and his/her account closed.";
            }
            catch (ArgumentException ae)
            {
                TempData[ErrorMessage] = ae.Message;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred while trying to delete employee from the system!";
            }

            return RedirectToAction("All", "Employee");
        }
    }
}
