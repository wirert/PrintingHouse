namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.MessageConstants;
    using static Core.Constants.ApplicationConstants;
    using Core.Models.Client;
    using Core.Services.Contracts;
    using Extensions;

    /// <summary>
    /// Client controller
    /// </summary>
    public class ClientController : BaseController
    {
        private readonly IClientService clientService;
        private readonly IEmployeeService employeeService;

        public ClientController(IClientService _clientService,
                        IEmployeeService _employeeService)
        {
            clientService = _clientService;
            employeeService = _employeeService;

        }

        /// <summary>
        /// Redirect to All clients
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        /// <summary>
        /// Adds new Client by merchant (Get)
        /// </summary>
        /// <returns>View with form for client details</returns>
        [HttpGet]
        [Authorize(Roles = MerchantRoleName)]
        public IActionResult Add()
        {
            var model = new AddClientViewModel();

            return View(model);
        }

        /// <summary>
        /// Adds new Client by merchant (Post)
        /// </summary>
        /// <returns>Redirect to all clients page</returns>
        [HttpPost]
        [Authorize(Roles = MerchantRoleName)]
        public async Task<IActionResult> Add(AddClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await clientService.ExistByName(model.Name))
            {
                TempData[WarningMessage] = "There is already a client with that name!";

                return RedirectToAction("All");
            }

            try
            {
                var userId = Guid.Parse(User.Id());

                var merchantId = await employeeService.GetIdByUserIdAsync(userId);

                model.MerchantId = merchantId;

                await clientService.AddNewAsync(model);

                TempData[SuccessMessage] = $"Successfully added client {model.Name}.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred while adding new client! Try again later.";
            }

            return RedirectToAction("All");
        }

        /// <summary>
        /// Show all active clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var clients = await clientService.GetAllAsync();

            return View(clients);
        }
    }
}
