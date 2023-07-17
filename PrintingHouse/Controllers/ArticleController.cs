namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Core.Models.Article;
    using static Core.Constants.MessageConstants;
    using Core.Services.Contracts;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IColorModelService colorModelService;
        private readonly IMaterialService materialService;
        private readonly IClientService clientService;

        public ArticleController(
                IArticleService _articleService,
                IColorModelService _colorModelService,
                IMaterialService _materialService,
                IClientService _clientService)
        {
            articleService = _articleService;
            colorModelService = _colorModelService;
            materialService = _materialService;
            clientService = _clientService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("All", "Client");
        }

        [HttpGet]
        public async Task<IActionResult> All(int? id = null)
        {
            //add button for recipe

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

        [HttpGet]
        public async Task<IActionResult> Add(int clientId)
        {
            var model = new ChooseArticleMaterialAndColorsViewModel();

            try
            {
                model.ClientId = clientId;

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
                var model = new CreateArticleViewModel()
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
        public async Task<IActionResult> Create(CreateArticleViewModel model)
        {
            if (await colorModelService.ExistByIdAsync(model.ColorModelId) == false ||
                await materialService.GetNameByIdIfExistAsync(model.MaterialId) == null)
            {
                TempData[WarningMessage] = "Material and Color model should be accurate!";

                return RedirectToAction("Add", model.ClientId);
            }

            if (model.DesignFile.Length == 0)
            {
                ModelState.AddModelError(nameof(model.DesignFile), "The design file is empty.");
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
    }
}

