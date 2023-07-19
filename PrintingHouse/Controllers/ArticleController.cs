namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Core.Models.Article;
    using static Core.Constants.MessageConstants;
    using Core.Services.Contracts;

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

        public ArticleController(
                IArticleService _articleService,
                IColorModelService _colorModelService,
                IMaterialService _materialService,
                IClientService _clientService,
                IFileService _fileService,
                IMaterialColorService _materialColorService)
        {
            articleService = _articleService;
            colorModelService = _colorModelService;
            materialService = _materialService;
            clientService = _clientService;
            fileService = _fileService;
            materialColorService = _materialColorService;
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
        public async Task<IActionResult> All(int? id = null)
        {
            try
            {
                var models = await articleService.GetAllAsync(id);

                if (models.Any() == false)
                {
                    return NotFound("There is no active articles");
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
            catch (Exception)
            {
                TempData[ErrorMessage] = "Problem retrieving articles.";

                return RedirectToAction("All", "Client");
            }
        }

        /// <summary>
        /// Download design file action
        /// </summary>
        /// <param name="fileName">Design file name</param>
        /// <param name="articleId">Article identifier</param>
        /// <returns>Design file</returns>
        public async Task<IActionResult> Download(string fileName, Guid articleId)
        {
            try
            {
                using var stream = await fileService.GetFileAsync(articleId, fileName);

                byte[] data = stream.ToArray();

                Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);

                return File(data, "application/octet-stream", fileName);
            }
            catch (Exception)
            {
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
        public async Task<IActionResult> Select(int clientId, Guid? articleId = null)
        {
            var model = new ChooseArticleMaterialAndColorsViewModel();

            try
            {
                model.ClientId = clientId;
                model.ArticleId = articleId;

                model = await articleService.FillSelectModelWithDataAsync(model);

                ViewData["MaterialsData"] = new SelectList(model.Materials.OrderBy(s => s.Id), "Id", "Type");
                ViewData["ColorModelsData"] = new SelectList(model.ColorModels.OrderBy(s => s.Id), "Id", "Name");

                return View(model);
            }
            catch (Exception)
            {
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
            var colorModelList = await colorModelService.GetColorModelByMaterialIdAsync(materialId);

            return Json(colorModelList);
        }

        /// <summary>
        /// Redirect to Create or Edit article actions
        /// </summary>
        /// <param name="materialColors">View model with selected material and color model</param>
        /// <returns>Redirect to Create or Edit article actions</returns>
        [HttpPost]
        public async Task<IActionResult> Select(ChooseArticleMaterialAndColorsViewModel materialColors)
        {
            if (await materialColorService.ExistByIds(materialColors.MaterialId, materialColors.ColorModelId) == false)
            {
                ModelState.AddModelError(string.Empty, "Invalid material or color model.");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    materialColors = await articleService.FillSelectModelWithDataAsync(materialColors);

                    return View(materialColors);
                }
                catch (Exception)
                {
                    TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";

                    return RedirectToAction("All", "Client");
                }
            }

            if (materialColors.ArticleId != null)
            {
                if (await articleService.ExistByIdAsync(materialColors.ArticleId) == false)
                {
                    return RedirectToAction("All", "Article");
                }

                TempData["MaterialId"] = materialColors.MaterialId;
                TempData["ColorModelId"] = materialColors.ColorModelId;

                return RedirectToAction("Edit", new { id = materialColors.ArticleId });
            }

            return RedirectToAction("Create", materialColors);
        }

        /// <summary>
        /// Create new Article (get)
        /// </summary>
        /// <param name="materialColors">View model with selected material and color model</param>
        /// <returns>View with View model</returns>
        [HttpGet]
        public async Task<IActionResult> Create(ChooseArticleMaterialAndColorsViewModel materialColors)
        {
            var materialName = await materialService.GetNameByIdIfExistAsync(materialColors.MaterialId);

            if (materialName == null ||
                await materialColorService.ExistByIds(materialColors.MaterialId, materialColors.ColorModelId) == false)
            {
                ModelState.AddModelError(string.Empty, "Material or color model is invalid!");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    materialColors = await articleService.FillSelectModelWithDataAsync(materialColors);

                    return RedirectToAction("Select", materialColors);
                }
                catch (Exception)
                {
                    TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";

                    return RedirectToAction("All", "Client");
                }
            }
            try
            {
                var model = new ArticleViewModel()
                {
                    MaterialId = materialColors.MaterialId,
                    MaterialName = materialName!,
                    ColorModelId = materialColors.ColorModelId,
                    ClientId = materialColors.ClientId,
                    ClientName = materialColors.ClientName
                };

                model.Colors = await colorModelService.GetColorModelColorsAsync(materialColors.ColorModelId);

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";
                return RedirectToAction("All", "Client");
            }
        }

        /// <summary>
        /// Creates a new article
        /// </summary>
        /// <param name="model">View model with form data from the view</param>
        /// <returns>Redirects to All clients</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            if (await materialColorService.ExistByIds(model.MaterialId, model.ColorModelId) == false)
            {
                TempData[WarningMessage] = "Material and Color model should be accurate!";

                return RedirectToAction("Select", model.ClientId);
            }

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
        /// <exception cref="ArgumentException">Thrown if material with given id or MaterialColorModel with given material id and color model id did'n exist</exception>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await articleService.GetByIdAsync(id);

                if (TempData.Peek("ColorModelId") != null)
                {
                    model.ColorModelId = (int)TempData["ColorModelId"]!;
                    model.MaterialId = (int)TempData["MaterialId"]!;

                    var materialName = await materialService.GetNameByIdIfExistAsync(model.MaterialId);

                    if (materialName == null ||
                        await materialColorService.ExistByIds(model.MaterialId, model.ColorModelId) == false)
                    {
                        throw new ArgumentException();
                    }

                    model.MaterialName = materialName;
                    model.Colors = await colorModelService.GetColorModelColorsAsync(model.ColorModelId);
                }

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to edit an article! Try again.";
                return RedirectToAction("All", "Article");
            }
        }

        /// <summary>
        /// Change article with data from form
        /// </summary>
        /// <param name="model">Article view model with form data</param>
        /// <returns>Redirect to action All</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ArticleViewModel model)
        {
            if (await materialColorService.ExistByIds(model.MaterialId, model.ColorModelId) == false)
            {
                TempData[WarningMessage] = "Material and Color model should be accurate!";

                return RedirectToAction("Select", model.ClientId);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                TempData[SuccessMessage] = $"Successfully edited article {model.Name}.";

                await articleService.EditAsync(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to edit an article! Try again.";
            }


            return RedirectToAction("All", "Article");
        }
    }
}

