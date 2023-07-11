namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Core.Contracts;
    using Core.Models.Article;
    using static Core.Constants.MessageConstants;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IColorModelService colorModelService;

        public ArticleController(IArticleService _articleService,
            IColorModelService _colorModelService)
        {
            articleService = _articleService;
            colorModelService = _colorModelService;
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

                model = await articleService.FillAddModelWithData(model);

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";
                return RedirectToAction("All", "Client");
            }
        }

        [HttpGet]
        public async Task<IActionResult> All(int? id = null)
        {


            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetColorsForAdding(int colorModelid)
        {
            try
            {
                var colors = await colorModelService.GetColorModelColorsAsync(colorModelid); 
                
                return PartialView("_ArticleColorsPartial", colors);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
