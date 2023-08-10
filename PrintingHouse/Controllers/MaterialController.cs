namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Core.Services.Contracts;
    using static Core.Constants.MessageConstants;

    public class MaterialController : BaseController
    {
        private readonly IMaterialService materialService;
        private readonly ILogger logger;

        public MaterialController(
            IMaterialService _materialService,
            ILogger<MaterialController> _logger)
        {
            materialService = _materialService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await materialService.GetAllMaterialsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int id, int quantity)
        {
            try
            {
                string materialName = await materialService.AddToStoreHouseAsync(id, quantity);

                TempData[SuccessMessage] = $"{quantity} pieces of {materialName} added to store house";
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                TempData[WarningMessage] = ae.Message;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Someting went wrong trying to add materials. Try again";
            }

            return RedirectToAction("Index");   
        }
    }
}
