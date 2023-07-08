namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Core.AdminModels.Employee;
    using Core.Contracts;
    using Infrastructure.Data.Entities.Account;
    using static Core.Constants.MessageConstants;

    public class EmployeeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IEmployeeService employeeService;
        private readonly IPositionService positionService;
        private readonly IAccountService accountService;

        public EmployeeController(
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
            IEmployeeService _employeeService,
            IPositionService _positionService,
            IAccountService _accountService)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            employeeService = _employeeService;
            positionService = _positionService;
            accountService = _accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var employeeUserIds = await employeeService.GetAllEmployeesUserIdAsync();

            var users = await accountService.GetAllNewEmployees(employeeUserIds);

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

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            var positions = await positionService.GetAllAsync();

            if (positions.All(p => p.Id != model.PositionId))
            {
                ModelState.AddModelError(nameof(model.PositionId), "Selected position does not exist!");
            }
            
            var user = await userManager.FindByIdAsync(model.ApplicationUserId.ToString());

            if (user == null || user.IsActive == false)
            {
                ModelState.AddModelError(nameof(model.ApplicationUserId), "Selected user does not exist!");
            }

            var role = roleManager.FindByNameAsync(model.Role);

            if (role == null)
            {
                ModelState.AddModelError(nameof(model.Role), "Selected role does not exist!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await userManager.AddToRoleAsync(user!, model.Role);

                    if (result.Succeeded)
                    {
                        await employeeService.AddAsync(model);

                        TempData[SuccessMessage] = "Employee was added successfully!";

                        return RedirectToAction("All");
                    }

                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add access for user!");
                }
                catch(InvalidOperationException)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add access for user!");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add employee!");
                }
            }            

            var employeeUserIds = await employeeService.GetAllEmployeesUserIdAsync();

            var users = await accountService.GetAllNewEmployees(employeeUserIds);
            var roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();

            model.Users = users;
            model.Positions = positions;
            model.AccessLevels = roles;

            return View(model);
        }

        public async Task<IActionResult> All()
        {
            var model = await employeeService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await employeeService.GetByIdAsync(id);

            if (model == null)
            {
                TempData[ErrorMessage] = "Employee with such Id does not exist!";

                return RedirectToAction("All");
            }

            try
            {
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

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeViewModel model)
        {
            var positions = await positionService.GetAllAsync();

            if (positions.All(p => p.Id != model.PositionId))
            {
                ModelState.AddModelError(nameof(model.PositionId), "Selected position does not exist!");
            }

            var user = await userManager.FindByIdAsync(model.ApplicationUserId.ToString());            

            if (user == null || user.IsActive == false)
            {
                ModelState.AddModelError(nameof(model.ApplicationUserId), "Selected employee does not exist!");
            }

            var role = roleManager.FindByNameAsync(model.Role);

            if (role == null)
            {
                ModelState.AddModelError(nameof(model.Role), "Selected role does not exist!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userRoles = await userManager.GetRolesAsync(user!);
                    var removeRolesResult = await userManager.RemoveFromRolesAsync(user!, userRoles);

                    if (!removeRolesResult.Succeeded)
                    {
                        throw new InvalidOperationException();
                    }

                    var addRoleResult = await userManager.AddToRoleAsync(user!, model.Role);

                    if (!addRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException();
                        
                    }

                    await employeeService.EditAsync(model);

                    TempData[SuccessMessage] = "Position was changed successfully!";

                    return RedirectToAction("All");
                }
                catch (InvalidOperationException)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add access for user!");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to change position!");
                }
            }

            var roles = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();

            model.Positions = positions;
            model.Roles = roles;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
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
