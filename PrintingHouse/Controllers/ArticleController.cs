namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PrintingHouse.Core.Models.Article;

    public class ArticleController : BaseController
    {
        public IActionResult Index()
        {
            return View("All");
        }

        public IActionResult Add()
        {
            var model = new AddArticleViewModel();

            return View(model);
        }

        //[HttpGet]
        //public Task<IActionResult> All(int? id = null)
        //{


        //    return Ok();
        //}
    }
}
