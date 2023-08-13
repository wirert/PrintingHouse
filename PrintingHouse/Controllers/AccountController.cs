namespace PrintingHouse.Controllers
{
    using System.Net;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Account;
    using Core.Constants;
    using Infrastructure.Data.Entities.Account;
    using static Core.Constants.ApplicationConstants;

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        

        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;            
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
                Email = WebUtility.HtmlEncode( model.Email),
                FirstName = WebUtility.HtmlEncode(model.FirstName),
                LastName = WebUtility.HtmlEncode(model.LastName),
                UserName = WebUtility.HtmlEncode(model.UserName),
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);
                        
            if (result.Succeeded)
            {
               var addclaimResult = await userManager.AddClaimAsync(user, new Claim(ApplicationConstants.FullNameClaim, $"{user.FirstName} {user.LastName}"));

                if (!addclaimResult.Succeeded)
                {
                    foreach (var error in addclaimResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                        await userManager.DeleteAsync(user);

                        return View(model);
                    }
                }

               await signInManager.SignInAsync(user, isPersistent: false);

                TempData[MessageConstants.SuccessMessage] = $"Welcome {user.FirstName} {user.LastName}!";

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
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, true);

                if (result.Succeeded)
                {
                    TempData[MessageConstants.SuccessMessage] = $"Welcome back {user.FirstName} {user.LastName}!";

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
    }
}