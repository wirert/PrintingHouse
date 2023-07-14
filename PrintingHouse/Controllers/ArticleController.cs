namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Core.Contracts;
    using Core.Models.Article;
    using static Core.Constants.MessageConstants;
    using Microsoft.AspNetCore.Http;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IColorModelService colorModelService;
        private readonly IFileService fileService;

        public ArticleController(IArticleService _articleService,
            IColorModelService _colorModelService,
            IFileService _fileService)
        {
            articleService = _articleService;
            colorModelService = _colorModelService;
            fileService = _fileService;

        }

        public IActionResult Index()
        {
            return View("All");
        }

        [HttpGet]
        public async Task<IActionResult> Add(int clientId)
        {
            var model = new AddArticleViewModel();

            try
            {
                model.ClientId = clientId;

                model = await articleService.FillAddModelWithDataAsync(model);

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";
                return RedirectToAction("All", "Client");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddArticleViewModel model)
        {
            if (model.DesignFile.Length == 0)
            {
                ModelState.AddModelError(nameof(model.DesignFile), "The design file is empty.");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    model = await articleService.FillAddModelWithDataAsync(model);

                    return View(model);
                }
                catch (Exception)
                {
                    TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";

                    return RedirectToAction("All", "Client");
                }
            }

            try
            {
                await articleService.AddAsync(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";
            }

                return RedirectToAction("All", "Client");
            }

        [HttpGet]
        public async Task<IActionResult> All(int? id = null)
        {


            return Ok();
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetColorsForAdding(int colorModelid, ICollection<AddArticleColorVeiwModel> colors)
            {
            try
            {
                colors = await colorModelService.GetColorModelColorsAsync(colorModelid);

                return PartialView("_ArticleColorsPartial", colors);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

