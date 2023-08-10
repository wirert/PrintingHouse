﻿namespace PrintingHouse.ViewComponents
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.ViewComponents.Models;
    using static Core.Constants.MessageConstants;

    public class ConsumablesViewComponent : ViewComponent
    {
        private readonly IMaterialService materialService;
        private readonly IArticleService articleService;
        private readonly IColorService colorService;
        private readonly ILogger logger;

        public ConsumablesViewComponent(
            IMaterialService _materialService,
            IArticleService _articleService,
            IColorService _colorService,
            ILogger<ConsumablesViewComponent> _logger)
        {
            materialService = _materialService;
            articleService = _articleService;
            colorService = _colorService;
            logger = _logger;

        }

        public async Task<IViewComponentResult> InvokeAsync(Guid articleId)
        {            
            try
            {
                var model = await GetConsumablesAsync(articleId);

                return View(model);
            }
            catch(ArgumentException ae)
            {
                logger.LogInformation(ae.Message);
                TempData[WarningMessage] = ae.Message;
                throw;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }

           
        }

        private async Task<ConsumablesViewModel> GetConsumablesAsync(Guid articleId)
        {              
            var artModel = await articleService.GetEditViewModelWithData(null, null, articleId);
            var materialInStock = await materialService.GetMaterialQuantityById(artModel.MaterialId);
            var colorsInStock = await colorService.GetAllAsync(artModel.ColorModelId);

            var model = new ConsumablesViewModel()
            {
                MaterialName = artModel.MaterialName,
                MaterialQuantityInStock = materialInStock,
                Colors = artModel.Colors
                    .Select(c => new ConsumableColorViewModel()
                    {
                        Name = c.ColorName,
                        NeededQuantity = c.ColorQuantity,
                        InStock = colorsInStock.Single(cq => cq.Name == c.ColorName).InStock
                    })
            };

            return model;
        }
    }
}
