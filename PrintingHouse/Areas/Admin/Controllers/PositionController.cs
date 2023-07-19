namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PrintingHouse.Core.AdminModels.Position;
    using PrintingHouse.Core.Services.Contracts;
    using static Core.Constants.MessageConstants;

    /// <summary>
    /// Work position controller (admin area)
    /// </summary>
    public class PositionController : BaseController
    {
        private readonly IPositionService positionService;

        public PositionController(IPositionService _positionService)
        {
            positionService = _positionService;
        }

        /// <summary>
        /// Add new position
        /// </summary>
        /// <returns>view with add position view model</returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPositionViewModel();

            return View(model);
        }

        /// <summary>
        /// Creates new work position
        /// </summary>
        /// <param name="model">Add position view model with form data</param>
        /// <returns>Redirect to action All in Employee controller</returns>
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

        /// <summary>
        /// Gets all active position to view to select for deletion
        /// </summary>
        /// <returns>view with Enumeration of Position view model</returns>
        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var positions = await positionService.GetAllAsync();

            return View(positions);
        }

        /// <summary>
        /// Soft delete position 
        /// </summary>
        /// <param name="positionId">position id</param>
        /// <returns>Redirect to action All in Employee controller</returns>
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
