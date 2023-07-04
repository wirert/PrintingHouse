namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Core.Contracts;
    using Core.Models.Employee;
    using Infrastructure.Data.Entities.Account;
    using static Core.Constants.MessageConstants;

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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var employeeUserIds = await employeeService.GetAllEmployeesUserIdAsync();

            var users = await userManager.Users
                .Where(u => u.IsActive && 
                        employeeUserIds.Contains(u.Id) == false)
                .Select(u => new AllUsersViewModel()
                {
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.ApplicationUserId.ToString());

            if (user == null)
            {
                TempData[ErrorMessage] = "Invalid user!";

                return View(model);
            }

            var role = roleManager.FindByNameAsync(model.Role);

            if (role == null)
            {
                TempData[ErrorMessage] = "Invalid access level!";

                return View(model);
            }

            var positions = await positionService.GetAllAsync();

            if (positions.All(p => p.Id != model.PositionId))
            {
                TempData[ErrorMessage] = "Invalid position!";

                return View(model);
            }

            var result = await userManager.AddToRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                await employeeService.AddAsync(model);

                return RedirectToAction("All");
            }

            

            return RedirectToAction("All");
        }
    }
}
