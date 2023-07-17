namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PrintingHouse.Core.AdminModels.Position;
    using PrintingHouse.Core.Services.Contracts;
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

            try
            {
                var positions = await positionService.GetAllAsync();

                if (positions.Any(p => p.Name == model.Name))
                {
                    throw new ArgumentException($"Position {model.Name} already exist!");
                }

                await positionService.AddNewAsync(model);

                TempData[SuccessMessage] = $"Successfully added position {model.Name}.";
            }
            catch(ArgumentException ae)
            {
                TempData[ErrorMessage] = ae.Message;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred while trying to add position!";                
            }

            return RedirectToAction("All", "Employee");
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var positions = await positionService.GetAllAsync();

            return View(positions);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int positionId)
        {
            try
            {
                var positions = await positionService.GetAllAsync();

                if (positions.All(p => p.Id != positionId))
                {
                    throw new ArgumentException("There is no such position!");
                }

                await positionService.DeleteAsync(positionId);

                TempData[SuccessMessage] = "You successfully deleted the position";
            }
            catch (ArgumentException ae)
            {
                TempData[ErrorMessage] = ae.Message;
            }
            catch (InvalidOperationException ioe)
            {
                TempData[ErrorMessage] = ioe.Message;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred while trying to delete position!";
            }

            return RedirectToAction("All", "Employee");
        }
    }
}
