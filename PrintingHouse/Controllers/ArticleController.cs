namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Core.Models.Article;
    using Core.Constants;
    using static Core.Constants.MessageConstants;
    using static Core.Constants.ApplicationConstants;
    using Core.Services.Contracts;
    using Infrastructure.Data.Entities.Enums;

    /// <summary>
    /// Article controller
    /// </summary>
    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IColorModelService colorModelService;
        private readonly IMaterialService materialService;
        private readonly IClientService clientService;
        private readonly IFileService fileService;
        private readonly IMaterialColorService materialColorService;
        private readonly ILogger logger;

        public ArticleController(
                IArticleService _articleService,
                IColorModelService _colorModelService,
                IMaterialService _materialService,
                IClientService _clientService,
                IFileService _fileService,
                IMaterialColorService _materialColorService,
                ILogger<ArticleController> _logger)
        {
            articleService = _articleService;
            colorModelService = _colorModelService;
            materialService = _materialService;
            clientService = _clientService;
            fileService = _fileService;
            materialColorService = _materialColorService;
            logger = _logger;
        }

        /// <summary>
        /// Redirect to All clients
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction("All", "Client");
        }

        /// <summary>
        /// Show all articles or All articles of client
        /// </summary>
        /// <param name="id">Clent Id (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All(Guid? id = null)
        {
            try
            {
                var models = await articleService.GetAllAsync(id);

                if (models.Any() == false)
                {
                    TempData[WarningMessage] = "There are no articles";

                    return RedirectToAction("All", "Client");
                }

                if (id.HasValue)
                {
                    ViewBag.Title = $"All articles of Client {models.First().ClientName}";
                }
                else
                {
                    ViewBag.Title = "All articles";
                }

                return View(models);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Problem retrieving articles.";

                return RedirectToAction("All", "Client");
            }
        }

        /// <summary>
        /// Download design file action
        /// </summary>
        /// <param name="articleId">Article identifier</param>
        /// <returns>Design file</returns>
        public async Task<IActionResult> Download(Guid articleId)
        {
            try
            {
                string fileName = await articleService.GetFileNameByIdAsync(articleId);

                using var stream = await fileService.GetFileAsync(articleId, fileName);

                byte[] data = stream.ToArray();

                Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);

                return File(data, "application/octet-stream", fileName);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                return NotFound("Error downloading file.");
            }
        }

        /// <summary>
        /// Choose Material and Color model (get)
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Select(Guid clientId, Guid? articleId = null)
        {
            try
            {
                if (articleId != null &&
                await articleService.ExistByIdAsync(articleId) == false)
                {
                    return BadRequest();
                }

                var model = new ChooseArticleMaterialAndColorsViewModel();

                model.ClientId = clientId;
                model.ArticleId = articleId;

                model = await articleService.FillSelectModelWithDataAsync(model);

                ViewData["MaterialsData"] = new SelectList(model.Materials.OrderBy(s => s.Id), "Id", "Type");
                ViewData["ColorModelsData"] = new SelectList(model.ColorModels.OrderBy(s => s.Id), "Id", "Name");

                return View(model);
            }
            catch (ArgumentNullException ane)
            {
                logger.LogInformation(ane, ane.Message);

                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";

                return RedirectToAction("All", "Client");
            }
        }

        /// <summary>
        /// Post method for Ajax request from select view.
        /// Return Collection of color models for a particular material.
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns>Collection of color models in Json format</returns>
        [HttpPost]
        public async Task<JsonResult> GetColorModelByMaterialId(string materialId)
        {
            try
            {
                var colorModelList = await colorModelService.GetColorModelByMaterialIdAsync(materialId);

                return Json(colorModelList);
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);

                return Json(new { Error = ae.Message });
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                return Json(new { Error = "Error getting color model for this material." });
            }
        }

        /// <summary>
        /// Redirect to Create or Edit article actions
        /// </summary>
        /// <param name="materialColors">View model with selected material and color model</param>
        /// <returns>Redirect to Create or Edit article actions</returns>
        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Select(ChooseArticleMaterialAndColorsViewModel materialColors)
        {
            try
            {
                if (!await materialColorService.ExistByIds(materialColors.MaterialId, materialColors.ColorModelId) ||
                    !await clientService.ExistsByIdAndNameAsync(materialColors.ClientId, materialColors.ClientName)
                   )
                {
                    logger.LogInformation("Arguments are altered.");
                    return BadRequest(ModelState);
                }

                TempData["MaterialId"] = materialColors.MaterialId;
                TempData["ColorModelId"] = materialColors.ColorModelId;

                if (materialColors.ArticleId != null)
                {
                    if (await articleService.ExistByIdAsync(materialColors.ArticleId) == false)
                    {
                        return BadRequest();
                    }
                    return RedirectToAction("Edit", new { id = materialColors.ArticleId });
                }

                TempData["ClientId"] = materialColors.ClientId;
                TempData["ClientName"] = materialColors.ClientName;

                return RedirectToAction("Create");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Create new Article (get)
        /// </summary>
        /// <returns>View with View model</returns>
        [HttpGet]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Create()
        {
            bool haveMaterialId = int.TryParse(TempData["MaterialId"]?.ToString(), out int materialId);
            bool haveColorModelId = int.TryParse(TempData["ColorModelId"]?.ToString(), out int colorModelId);
            bool haveClientId = Guid.TryParse(TempData["ClientId"]?.ToString(), out Guid clientId);
            var clientName = TempData["ClientName"]?.ToString();

            if (haveMaterialId == false || haveColorModelId == false )
            {
                if (!haveClientId)
                {
                    logger.LogInformation("Client Id is not a valid Guid");
                    TempData[WarningMessage] = "First choose material and color model!";
                    return RedirectToAction("Select", new { clientId });
                }

                TempData[WarningMessage] = "To Create Article, first choose owner!";

                return RedirectToAction("All", "Client");
            }

            try
            {
               var model = await articleService.GetCreateViewModelWithData(materialId, colorModelId, clientId, clientName);

                return View(model);
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates a new article
        /// </summary>
        /// <param name="model">View model with form data from the view</param>
        /// <returns>Redirects to All clients</returns>
        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            if (model.DesignFile == null || model.DesignFile.Length == 0)
            {
                ModelState.AddModelError(nameof(model.DesignFile), "Upload a valid design file");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await articleService.CreateAsync(model);

                TempData[SuccessMessage] = $"Successfully added article {model.Name}";
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                return BadRequest();
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";
            }

            return RedirectToAction("All", "Client");
        }

        /// <summary>
        /// Edit a particular article
        /// </summary>
        /// <param name="id">Article identifier</param>
        /// <returns>View with Article view model</returns>
        [HttpGet]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await articleService.GetByIdAsync(id);

                if (TempData.Peek("ColorModelId") != null)
                {
                    model.ColorModelId = (int)TempData["ColorModelId"]!;
                    model.MaterialId = (int)TempData["MaterialId"]!;

                    var material = await materialService.GetMaterialByIdAsync(model.MaterialId);

                    if (material == null ||
                        !await materialColorService.ExistByIds(model.MaterialId, model.ColorModelId)
                        )
                    {
                        return BadRequest();
                    }

                    model.MaterialName = material.Type;
                    model.MeasureUnit = (MeasureUnit)material.MeasureUnit!;

                    if (model.MeasureUnit == MeasureUnit.Piece)
                    {
                        model.Length = ModelConstants.Article_Piece_Length;
                    }

                    model.Colors = await colorModelService.GetColorModelColorsAsync(model.ColorModelId);
                }

                return View(model);
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }

        /// <summary>
        /// Change article with data from form
        /// </summary>
        /// <param name="model">Article view model with form data</param>
        /// <returns>Redirect to action All</returns>
        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Edit(ArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await articleService.ExistByIdAsync(model.Id) ||
                !await materialColorService.ExistByIds(model.MaterialId, model.ColorModelId) ||
                !await clientService.ExistsByIdAndNameAsync(model.ClientId, model.ClientName)
                )
            {
                return BadRequest();
            }

            try
            {
                await articleService.EditAsync(model);

                TempData[SuccessMessage] = $"Successfully edited article {model.Name}.";
            }
            catch (ArgumentException ae)
            {
                TempData[ErrorMessage] = ae.Message;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to edit an article! Try again.";
            }

            return RedirectToAction("All", "Article");
        }

        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await articleService.DeleteByIdAsync(id);

                TempData[SuccessMessage] = "Article successfully deleted";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unable to delete article! Try again later";
            }

            return RedirectToAction("All");
        }
    }
}

