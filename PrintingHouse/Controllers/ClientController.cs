namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.MessageConstants;
    using static Core.Constants.ApplicationConstants;
    using Core.Exceptions;
    using Core.Models.Client;
    using Core.Services.Contracts;
    using Extensions;

    /// <summary>
    /// Client controller
    /// </summary>
    public class ClientController : BaseController
    {
        private readonly IClientService clientService;
        private readonly ILogger logger;

        public ClientController(
                        IClientService _clientService,
                        ILogger<ClientController> _logger)
        {
            clientService = _clientService;
            logger = _logger;
        }

        /// <summary>
        /// Permanent redirects  to All clients
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectPermanent("All");
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

            try
            {
                Guid userId = User.Id();

                await clientService.AddNewAsync(model, userId);

                TempData[SuccessMessage] = $"Successfully added client {model.Name}.";
            }
            catch (ClientNameExistsException cnee)
            {
                TempData[WarningMessage] = cnee.Message;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Unexpected error occurred while adding new client! Try again later.";
            }

            return RedirectToAction("All");
        }

        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await clientService.DeleteAsync(id);

                TempData[SuccessMessage] = "Successfully deleted client and his articles";
            }
            catch (DeleteClientException dce)
            {
                TempData[WarningMessage] = dce.Message;
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae.Message);
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Problem deleting client! Try again";
            }

            return RedirectToAction("All");
        }
    }
}
