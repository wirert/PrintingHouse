namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PrintingHouse.Core.AdminModels.Position;
    using PrintingHouse.Core.Contracts;

    using static Core.Constants.MessageConstants;

    public class PositionController : BaseController
    {
        private readonly IPositionService positionService;

        public PositionController(IPositionService _positionService)
        {
            positionService = _positionService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPositionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPositionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var positions = await positionService.GetAllAsync();

            if (positions.Any(p => p.Name == model.Name))
            {                
                ViewData[ErrorMessage] = "This position already exist!";                
            }
            else
            {
                try
                {
                    await positionService.AddNewAsync(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unexpected error occurred while trying to add position!");

                    return View(model);
                }
            }

            return RedirectToAction("All", "Employee");
        }
    }
}
