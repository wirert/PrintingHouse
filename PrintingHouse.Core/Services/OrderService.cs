namespace PrintingHouse.Core.Services
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Models.Order;
    using PrintingHouse.Infrastructure.Data.Entities.Enums;

    public class OrderService : IOrderService
    {
        private readonly IRepository repo;

        public OrderService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<AddOrderViewModel> CreateAddModelByArticleIdAsync(Guid articleId)
        {
            var article = await repo.GetByIdAsync<Article>(articleId);

            if (article == null || article.IsActive == false)
            {
                throw new ArgumentException();
            }

            return new AddOrderViewModel()
            {
                ArticleId = articleId,
                ArticleName = article.Name
            };
        }

        public async Task CreateOrder(AddOrderViewModel model)
        {
            var article = await repo.All<Article>(a => a.Id == model.ArticleId && a.IsActive)
                .Include(a => a.ArticleColors)
                .ThenInclude(ac => ac.Color)
                .Include(a => a.MaterialColorModel)
                .ThenInclude(mc => mc.Machines)
                .ThenInclude(m => m.OrdersQueue)
                .SingleAsync();

            var order = new Order()
            {
                ArticleId = model.ArticleId,
                Article = article,
                EndDate = model.EndDate,
                Comment = model.Comment,
                Quantity = model.Quantity
            };

            order.Status = TakeMaterialsAndColorsIfAvailable(article, model.Quantity) ? OrderStatus.Waiting : OrderStatus.NoConsumable;

            var machine = await repo.All<Machine>(m => m.MaterialId == article.MaterialId && m.ColorModelId == article.ColorModelId)
                .OrderBy(m => m.OrdersQueue.Last().ExpectedPrintDate)
                .ThenBy(m => m.OrdersQueue.Count)
                .FirstOrDefaultAsync();
                



            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Take needed quantity of material and colors if there is enough in stock
        /// </summary>
        /// <param name="article">Order article</param>
        /// <param name="neededArticleQuantity">Order quantity of articles</param>
        /// <returns>Whether operation is successfull</returns>
        private bool TakeMaterialsAndColorsIfAvailable(Article article, double neededArticleQuantity)
        {
            var materialNeeded = article.MaterialQuantity * neededArticleQuantity;

            var materialInStock = repo.All<Material>(m => m.Id == article.MaterialId && m.IsActive).Single().InStock;

            if (materialInStock < (int)Math.Ceiling(materialNeeded))
            {
                return false;
            }

            foreach (ArticleColor color in article.ArticleColors)
            {
                var neededColor = color.ColorQuantity * neededArticleQuantity;

                if (color.Color.InStock < (int)Math.Ceiling(neededColor))
                {
                    return false;
                }
            }

            materialInStock -= (int)Math.Ceiling(materialNeeded);

            foreach (ArticleColor color in article.ArticleColors)
            {
                var neededColor = color.ColorQuantity * neededArticleQuantity;

                color.Color.InStock -= (int)Math.Ceiling(neededColor);
            }

            return true;
        }
    }
}
