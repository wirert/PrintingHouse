﻿namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Core.AdminModels.Position;
    using Core.Services.Contracts;
    using static Core.Constants.MessageConstants;

    /// <summary>
    /// Work position controller (admin area)
    /// </summary>
    public class PositionController : BaseController
    {
        private readonly IPositionService positionService;
        private readonly ILogger logger;

        public PositionController(
            IPositionService _positionService, 
            ILogger<PositionController> _logger)
        {
            positionService = _positionService;
            logger = _logger;
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
                await positionService.AddNewAsync(model);

                TempData[SuccessMessage] = $"Successfully added position {model.Name}.";
            }
            catch(ArgumentException ae)
            {
                TempData[ErrorMessage] = ae.Message;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
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
                await positionService.DeleteAsync(positionId);

                TempData[SuccessMessage] = "You successfully deleted the position";
            }
            catch (InvalidOperationException ioe)
            {
                TempData[ErrorMessage] = ioe.Message;
            }
            catch (ArgumentException ae)
            {
                TempData[ErrorMessage] = ae.Message;
            }            
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Unexpected error occurred while trying to delete position!";
            }

            return RedirectToAction("All", "Employee");
        }
    }
}
