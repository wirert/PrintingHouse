namespace PrintingHouse.Core.Services
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Infrastructure.Data.Entities.Enums;
    using Models.Order;

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
                Quantity = model.Quantity,

            };

            order.Status = TakeMaterialsAndColorsIfAvailable(article, model.Quantity) ? OrderStatus.Waiting : OrderStatus.NoConsumable;

            var machine = article.MaterialColorModel.Machines
                .OrderBy(m => m.OrdersQueue.LastOrDefault(order).ExpectedPrintDate)
                .ThenBy(m => m.OrdersQueue.Count)
                .FirstOrDefault();

            if (machine == null)
            {
                throw new ArgumentException("No machine availabe!");
            }

            order.Machine = machine;
            order.ExpectedPrintTime = machine.PrintTime * order.Quantity * article.MaterialQuantity;

            SetExpectedPrintDate(order);

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Set expected print date according print time and other orders
        /// </summary>
        /// <param name="order">The order</param>
        private static void SetExpectedPrintDate(Order order)
        {
            var lastOrderInMachine = order.Machine!.OrdersQueue.LastOrDefault();

            if (lastOrderInMachine == null)
            {
                order.ExpectedPrintDate = DateTime.UtcNow.Date;

                if ((DateTime.UtcNow.TimeOfDay + order.ExpectedPrintTime) > TimeSpan.FromHours(18))
                {
                    order.ExpectedPrintDate.AddDays(1);
                }                
            }
            else
            {
                var ordersForDay = order.Machine.OrdersQueue
                    .Where(o => o.ExpectedPrintDate == lastOrderInMachine.ExpectedPrintDate)
                    .ToList();

                TimeSpan totalPrintTimeNeeded = TimeSpan.Zero;

                foreach (var queueOrder in ordersForDay)
                {
                    totalPrintTimeNeeded += queueOrder.ExpectedPrintTime;
                }

                order.ExpectedPrintDate = lastOrderInMachine.ExpectedPrintDate;

                if (totalPrintTimeNeeded + order.ExpectedPrintTime > TimeSpan.FromHours(10))
                {                
                    order.ExpectedPrintDate.AddDays(1);

                    if (order.ExpectedPrintDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        order.ExpectedPrintDate.AddDays(2);
                    }
                }
            }
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
