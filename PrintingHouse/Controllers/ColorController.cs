namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Services.Contracts;
    using static Core.Constants.MessageConstants;
    using static Core.Constants.ApplicationConstants;

    public class ColorController : BaseController
    {
        private readonly IColorService colorService;
        private readonly ILogger logger;

        public ColorController(
            IColorService _colorService, 
            ILogger<ColorController> _logger)
        {
            colorService = _colorService;
            logger = _logger;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await colorService.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> Buy(int id, int quantity)
        {
            try
            {
                string colorName = await colorService.AddToStoreHouseAsync(id, quantity);

                TempData[SuccessMessage] = $"{quantity} pieces of {colorName} added to store house";
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                TempData[WarningMessage] = ae.Message;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Someting went wrong trying to add color. Try again";
            }

            return RedirectToAction("Index");
        }
    }
}
