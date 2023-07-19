namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Core.Models.Article;
    using static Core.Constants.MessageConstants;
    using Core.Services.Contracts;
    using System.Net.Mime;
    using System.IO;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IColorModelService colorModelService;
        private readonly IMaterialService materialService;
        private readonly IClientService clientService;
        private readonly IFileService fileService;

        public ArticleController(
                IArticleService _articleService,
                IColorModelService _colorModelService,
                IMaterialService _materialService,
                IClientService _clientService,
                IFileService _fileService)
        {
            articleService = _articleService;
            colorModelService = _colorModelService;
            materialService = _materialService;
            clientService = _clientService;
            fileService = _fileService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("All", "Client");
        }

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

        [HttpGet]
        public async Task<IActionResult> Add(int clientId, Guid? articleId = null)
        {
            var model = new ChooseArticleMaterialAndColorsViewModel();

            try
            {
                model.ClientId = clientId;
                model.ArticleId = articleId;

                model = await articleService.FillAddModelWithDataAsync(model);

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

        [HttpPost]
        public async Task<JsonResult> GetColorModelByMaterialId(string materialId)
        {
            var colorModelList = await colorModelService.GetColorModelByMaterialIdAsync(materialId);

            return Json(colorModelList);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChooseArticleMaterialAndColorsViewModel materialColors)
        {
            if (await colorModelService.ExistByIdAsync(materialColors.ColorModelId) == false)
            {
                ModelState.AddModelError(nameof(materialColors.ColorModelId), "Color model is invalid!");
            }

            if (await materialService.GetNameByIdIfExistAsync(materialColors.MaterialId) == null)
            {
                ModelState.AddModelError(nameof(materialColors.MaterialId), "Material is invalid!");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    materialColors = await articleService.FillAddModelWithDataAsync(materialColors);

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

                //Guid id =(Guid)materialColors.ArticleId;

                return RedirectToAction("Edit", new { id = materialColors.ArticleId });
            }

            return RedirectToAction("Create", materialColors);
        }


        [HttpGet]
        public async Task<IActionResult> Create(ChooseArticleMaterialAndColorsViewModel materialColors)
        {
            if (await colorModelService.ExistByIdAsync(materialColors.ColorModelId) == false)
            {
                ModelState.AddModelError(nameof(materialColors.ColorModelId), "Color model is invalid!");
            }

            var materialName = await materialService.GetNameByIdIfExistAsync(materialColors.MaterialId);

            if (materialName == null)
            {
                ModelState.AddModelError(nameof(materialColors.MaterialId), "Material is invalid!");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    materialColors = await articleService.FillAddModelWithDataAsync(materialColors);

                    return RedirectToAction("Add", materialColors);
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

        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            if (await colorModelService.ExistByIdAsync(model.ColorModelId) == false ||
                await materialService.GetNameByIdIfExistAsync(model.MaterialId) == null)
            {
                TempData[WarningMessage] = "Material and Color model should be accurate!";

                return RedirectToAction("Add", model.ClientId);
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
                        await colorModelService.ExistByIdAsync(model.ColorModelId) == false)
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

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleViewModel model)
        {
            if (await colorModelService.ExistByIdAsync(model.ColorModelId) == false ||
                await materialService.GetNameByIdIfExistAsync(model.MaterialId) == null)
            {
                TempData[WarningMessage] = "Material and Color model should be accurate!";

                return RedirectToAction("Add", model.ClientId);
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

