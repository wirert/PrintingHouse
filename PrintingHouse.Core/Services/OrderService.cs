namespace PrintingHouse.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Constants;
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

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            var models = await repo.AllReadonly<Order>()                
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    ArticleNo = o.Article.ArticleNumber,
                    ArticleName = o.Article.Name,
                    ClientName = o.Article.Client.Name,
                    Quantity = o.Quantity,
                    Material = o.Article.MaterialColorModel.Material.Type,
                    MeasureUnit = o.Article.MaterialColorModel.Material.MeasureUnit,
                    MaterialLength = o.Article.MaterialColorModel.Material.Lenght,
                    MaterialQuantity = (o.Quantity * o.Article.Length).ToString("f2"),
                    ColorModel = o.Article.MaterialColorModel.ColorModel.Name,
                    Width = o.Article.MaterialColorModel.Material.Width * ModelConstants.Kilometers_Meters,
                    EndDate = o.EndDate,
                    ExpectedPrintDate = o.ExpectedPrintDate,
                    Comment = o.Comment,
                    ExpectedPrintTime = o.ExpectedPrintTime,
                    OrderTime = o.OrderTime,
                    Status = o.Status
                })
                .ToListAsync();

            foreach (var model in models)
            {        
                if (model.MeasureUnit == MeasureUnit.km)
                {
                    model.EmbeddedMaterialCount = (int)Math.Ceiling(double.Parse(model.MaterialQuantity) / model.MaterialLength);
                    model.ScrappedMaterial = ((1 - (double.Parse(model.MaterialQuantity) / (model.EmbeddedMaterialCount * model.MaterialLength))) * 100).ToString();
                }
                else
                {
                    model.EmbeddedMaterialCount = (int)double.Parse(model.MaterialQuantity);
                    model.ScrappedMaterial = 0d.ToString();
                }
            }

            return models;
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
                .Include(a => a.MaterialColorModel)
                .ThenInclude(mc => mc.Material)
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
            order.ExpectedPrintTime = machine.PrintTime * order.Quantity * article.Length;

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

                if ((DateTime.UtcNow.TimeOfDay + order.ExpectedPrintTime) > TimeSpan.FromHours(18d))
                {
                    order.ExpectedPrintDate = order.ExpectedPrintDate.AddDays(1d);

                    if (order.ExpectedPrintDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        order.ExpectedPrintDate = order.ExpectedPrintDate.AddDays(2d);
                    }
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
                    order.ExpectedPrintDate = order.ExpectedPrintDate.AddDays(1d);

                    if (order.ExpectedPrintDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        order.ExpectedPrintDate = order.ExpectedPrintDate.AddDays(2d);
                    }
                }
            }
        }

        /// <summary>
        /// Take needed quantity of material and colors if there is enough in stock
        /// </summary>
        /// <param name="article">Order article</param>
        /// <param name="neededOrderArticleQuantity">Order quantity of articles</param>
        /// <returns>Whether operation is successfull</returns>
        private bool TakeMaterialsAndColorsIfAvailable(Article article, int neededOrderArticleQuantity)
        {
            var material = article.MaterialColorModel.Material;

            int materialQuantityNeeded = CalculateNeededOrderMaterialUnits(article, neededOrderArticleQuantity);

            if (material.InStock < materialQuantityNeeded)
            {
                return false;
            }

            foreach (ArticleColor color in article.ArticleColors)
            {
                double neededColor = color.ColorQuantity * neededOrderArticleQuantity;

                if (color.Color.InStock < (int)Math.Ceiling(neededColor))
                {
                    return false;
                }
            }

            material.InStock -= materialQuantityNeeded;

            foreach (ArticleColor color in article.ArticleColors)
            {
                var neededColor = color.ColorQuantity * neededOrderArticleQuantity;

                color.Color.InStock -= (int)Math.Ceiling(neededColor);
            }

            return true;
        }

        private int CalculateNeededOrderMaterialUnits(Article article, int neededOrderArticleQuantity)
        {
            var material = article.MaterialColorModel.Material;

            double materialQuantityNeeded = article.Length * neededOrderArticleQuantity;

            if (material.MeasureUnit == MeasureUnit.km)
            {
                materialQuantityNeeded /= material.Lenght;
            }

            return (int)Math.Ceiling(materialQuantityNeeded);
        }
    }
}
