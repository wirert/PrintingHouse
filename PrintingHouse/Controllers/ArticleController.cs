﻿namespace PrintingHouse.Controllers
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

            if (await colorModelService.ExistByIdAsync(model.ColorModelId) == false)
            {
                ModelState.AddModelError(nameof(model.ColorModelId), "Color model is invalid!");
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

                return RedirectToAction("AddColors", new
                {
                    colorModelId = model.ColorModelId,
                    article = model.Name,
                    client = model.ClientName
                });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong trying to add article! Try again.";

                return RedirectToAction("All", "Client");
            }


        }       

        [HttpGet]
        public async Task<IActionResult> AddColors(int colorModelId, string article, string client)
        {
            try
            {
                var colors = await colorModelService.GetColorModelColorsAsync(colorModelId);

                foreach (var color in colors)
                {
                    color.ArticleName = article;
                    color.ClientName = client;

                }
                
                return View(colors);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Unable to add recipe to article {article}. Try again later.";

                return RedirectToAction("All");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddColors(List<AddArticleColorVeiwModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await articleService.AddColorRecipeAsync(model);

                TempData[SuccessMessage] = $"Successfully added recipe to article {model.First().ArticleName}";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Unable to add recipe to article {model.First().ArticleName}. Try again later.";
            }

            return RedirectToAction("All");
        }
    }
}

