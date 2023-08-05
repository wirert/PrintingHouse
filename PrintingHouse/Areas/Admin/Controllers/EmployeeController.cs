namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Core.AdminModels.Employee;
    using static Core.Constants.MessageConstants;
    using Core.Exceptions;
    using Core.Services.Contracts;
    using Extensions;
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
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
            IEmployeeService _employeeService,
            IPositionService _positionService,
            ILogger<EmployeeController> _logger)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            employeeService = _employeeService;
            positionService = _positionService;
            logger = _logger;
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
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
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
                if (!ModelState.IsValid)
                {
                    var users = await employeeService.GetAllNewEmployees();
                    var roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();
                    var positions = await positionService.GetAllAsync();

                    model.Users = users;
                    model.Positions = positions;
                    model.AccessLevels = roles;

                    return View(model);
                }

                await employeeService.AddAsync(model);

                TempData[SuccessMessage] = "Employee was added successfully!";

                return RedirectToAction("All");
            }
            catch (ArgumentException ae)
            {
                logger.LogWarning(ae, ae.Message);

                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

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
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
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
                if (!ModelState.IsValid)
                {
                    model.Positions = await positionService.GetAllAsync();
                    model.Roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();

                    return View(model);
                }

                await employeeService.EditAsync(model, User.Id());

                TempData[SuccessMessage] = "Position was changed successfully!";
            }
            catch (EmployeeSelfChangeException esce)
            {
                TempData[WarningMessage] = esce.Message;
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Unexpected error occurred while trying to change position!";
            }

            return RedirectToAction("All", "Employee");
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
                await employeeService.DeleteAsync(id, User.Id());

                TempData[SuccessMessage] = $"Employee was successfully dismissed and his/her account closed.";
            }
            catch (EmployeeSelfChangeException esce)
            {
                TempData[WarningMessage] = esce.Message;
            }
            catch (ArgumentException ae)
            {
                logger.LogWarning(ae, ae.Message);
                TempData[ErrorMessage] = ae.Message;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Unexpected error occurred while trying to delete employee from the system!";
            }

            return RedirectToAction("All", "Employee");
        }
    }
}
