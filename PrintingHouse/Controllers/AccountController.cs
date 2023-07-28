namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Core.Models.Account;
    using static Core.Constants.RoleNamesConstants;
    using static Core.Constants.MessageConstants;
    using Infrastructure.Data.Entities.Account;
    using System.Security.Claims;

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IHostEnvironment hostEnvironment;

        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
            IHostEnvironment _hostEnvironment)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            hostEnvironment = _hostEnvironment;
        }

        /// <summary>
        /// Register action for registration of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        /// <summary>
        /// Register action for registration of users
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);

            
                        
            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        /// <summary>
        /// User Logging in
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        /// <summary>
        /// User Logging in
        /// </summary>
        /// <param name="model">Log in view model</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                        
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, false);

                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login");
            }

            return View(model);
        }

        /// <summary>
        /// User log out action
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Add admin role to user(only for development purpose)
        /// </summary>
        /// <returns>Redirect to Index action in Home controller</returns>        
        public async Task<IActionResult> AddRoles()
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            await roleManager.CreateAsync(new IdentityRole<Guid>(Admin));
            await roleManager.CreateAsync(new IdentityRole<Guid>(Employee));
            await roleManager.CreateAsync(new IdentityRole<Guid>(Merchant));

            var user = await userManager.Users.FirstAsync(u => u.Id == Guid.Parse("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"));

            await userManager.AddToRoleAsync(user!, Admin);

            TempData[SuccessMessage] = "Admin role added to Admin123";

            return RedirectToAction("Index", "Home");
        }       
    }
}