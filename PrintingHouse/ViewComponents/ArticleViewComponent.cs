namespace PrintingHouse.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using PrintingHouse.Core.Services.Contracts;

    public class ArticleViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;

        public ArticleViewComponent(IArticleService _articleService)
        {
            articleService = _articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View();
        }
    }
}
