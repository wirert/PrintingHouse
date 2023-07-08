namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.MessageConstants;
    using static Core.Constants.RoleNamesConstants;
    using Core.Contracts;
    using Core.Models.Client;
    using Extensions;

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

        [HttpGet]
        [Authorize(Roles = Merchant)]
        public IActionResult Add()
        {
            var model = new AddClientViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Merchant)]
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

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View();
        }
    }
}
