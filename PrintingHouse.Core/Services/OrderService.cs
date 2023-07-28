﻿namespace PrintingHouse.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Infrastructure.Data.Entities.Enums;
    using Models.Order;
    using Microsoft.AspNetCore.Identity;

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
                    Width = o.Article.MaterialColorModel.Material.Width,
                    EndDate = o.EndDate,
                    ExpectedPrintDate = o.ExpectedPrintDate,
                    Comment = o.Comment,
                    ExpectedPrintTime = o.ExpectedPrintDuration,
                    OrderTime = o.OrderTime,
                    Status = o.Status
                })
                .ToListAsync();

            foreach (var model in models)
            {
                if (model.MeasureUnit == MeasureUnit.m)
                {
                    model.EmbeddedMaterialCount = (int)Math.Ceiling(double.Parse(model.MaterialQuantity) / model.MaterialLength);
                    model.ScrappedMaterial = ((1 - (double.Parse(model.MaterialQuantity) / (model.EmbeddedMaterialCount * model.MaterialLength))) * 100).ToString("f0");
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
                Quantity = model.Quantity
            };

            order.Status = TakeMaterialsAndColorsIfAvailable(article, model.Quantity) ? OrderStatus.Waiting : OrderStatus.NoConsumable;

            var machine = article.MaterialColorModel.Machines
                .Where(m => m.Status == MachineStatus.Working)
                .OrderBy(m => m.OrdersQueue.LastOrDefault(order).ExpectedPrintDate)
                .ThenBy(m => m.OrdersQueue.Count)
                .ThenBy(m => m.PrintTime)
                .FirstOrDefault();

            if (machine == null)
            {
                throw new ArgumentException("No machine availabe!");
            }

            order.Machine = machine;
            order.ExpectedPrintDuration = machine.PrintTime * order.Quantity * article.Length / machine.MaterialPerPrint;

            SetExpectedPrintDate(order);

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();
        }

        public async Task ChangeStatusAsync(int id, OrderStatus status)
        {
            var order = await repo.GetByIdAsync<Order>(id);

            if (order == null)
            {
                throw new ArgumentException("Error finding order");
            }

            switch (order.Status)
            {
                case OrderStatus.Waiting:
                    switch (status)
                    {
                        case OrderStatus.Waiting:
                            break;
                        case OrderStatus.Printing: 
                            var machineOrders = await repo.AllReadonly<Order>(o => o.MachineId == order.MachineId).ToArrayAsync();

                            if (machineOrders.Any(o => o.Status == OrderStatus.Printing))
                            {
                                throw new ArgumentException("The machine is buzy! Wait to finish current print.");
                            }

                            break;
                        case OrderStatus.Canceled:

                            var articleOrderToCancel = await repo.All<Article>(a => a.Id == order.ArticleId)
                                .Include(a => a.MaterialColorModel)
                                .ThenInclude(mc => mc.Material)
                                .Include(a => a.ArticleColors)
                                .ThenInclude(ac => ac.Color)
                                .FirstAsync();
                            var materialCount = this.CalculateNeededOrderMaterialUnits(articleOrderToCancel, order.Quantity);

                            articleOrderToCancel.MaterialColorModel.Material.InStock += materialCount;

                            foreach (ArticleColor color in articleOrderToCancel.ArticleColors)
                            {
                                int returnColorQuantiry = (int)Math.Ceiling(color.ColorQuantity * order.Quantity);

                                color.Color.InStock += returnColorQuantiry;
                            }

                            order.MachineId = null;
                            order.Status = status;

                            await repo.SaveChangesAsync();

                            int materialId = articleOrderToCancel.MaterialId;
                            int colorModelId = articleOrderToCancel.ColorModelId;

                            await RearangeAllOrderOfParticularTypeAsync(materialId, colorModelId);

                            return;
                        default:
                            throw new ArgumentException("Can't change to this status!");
                    }
                    order.Status = status;
                    break;
                case OrderStatus.NoConsumable:
                    var article = await repo.All<Article>(a => a.Id == order.ArticleId)
                        .Include(a => a.MaterialColorModel)
                        .ThenInclude(mc => mc.Material)
                        .Include(a => a.ArticleColors)
                        .ThenInclude(ac => ac.Color)
                        .FirstAsync();

                    switch (status)
                    {
                        case OrderStatus.Completed:
                            throw new ArgumentException("This order is waiting for print!");
                        case OrderStatus.NoConsumable:
                            break;
                        case OrderStatus.Canceled:
                            order.MachineId = null;
                            order.Status = status;

                            await repo.SaveChangesAsync();

                            int materialId = article.MaterialId;
                            int colorModelId = article.ColorModelId;

                            await RearangeAllOrderOfParticularTypeAsync(materialId, colorModelId);

                            return;
                        case OrderStatus.Printing:
                            throw new ArgumentException("There is no enough consumables!");
                        default:
                            order.Status = this.TakeMaterialsAndColorsIfAvailable(article, order.Quantity) ? status : OrderStatus.NoConsumable;
                            break;
                    }
                    break;
                case OrderStatus.Printing:
                    switch (status)
                    {
                        case OrderStatus.Completed:
                        case OrderStatus.Canceled:
                            order.MachineId = null;
                            break;
                        case OrderStatus.Printing:
                            break;
                        default:
                            throw new ArgumentException("This order is printing!");
                    }
                    order.Status = status;
                    break;
                default:
                    throw new ArgumentException("Can't change the status of this order!");
            }

            await repo.SaveChangesAsync();
        }

        public async Task RearangeAllOrderOfParticularTypeAsync(int materialId, int colorModelId, int? orderId = null)
        {
            var orders = await repo
                .All<Order>(o => o.Article.MaterialId == materialId
                              && o.Article.ColorModelId == colorModelId
                             && (o.Status == OrderStatus.Waiting || o.Status == OrderStatus.NoConsumable))
                .Include(o => o.Article)
                .Include(o => o.Machine)
                .OrderBy(o => o.OrderTime)
                .ToListAsync();

            var machines = await repo
                .All<Machine>(m => m.MaterialId == materialId
                                && m.ColorModelId == colorModelId
                                && m.Status == MachineStatus.Working)
                .Include(m => m.OrdersQueue)
                .ToListAsync();

            foreach (var order in orders)
            {
                order.Machine!.OrdersQueue.Remove(order);
                order.MachineId = null;
                order.Machine = null;
            }

            if (orderId != null)
            {
                var priorityOrder = orders.First(o => o.Id == orderId);
                priorityOrder.Machine = machines
                            .OrderBy(m => m.OrdersQueue.Sum(o => o.ExpectedPrintDuration.Ticks))
                            .ThenBy(m => m.OrdersQueue.Count)
                            .ThenBy(m => m.PrintTime)
                            .First();
                priorityOrder.MachineId = priorityOrder.Machine.Id;
                priorityOrder.ExpectedPrintDuration = priorityOrder.Machine.PrintTime * priorityOrder.Quantity * priorityOrder.Article.Length / priorityOrder.Machine.MaterialPerPrint;

                SetExpectedPrintDate(priorityOrder);

                priorityOrder.Machine.OrdersQueue.Add(priorityOrder);
            }

            foreach (var order in orders.Where(o => o.Id != orderId))
            {
                order.Machine = machines
                            .OrderBy(m => m.OrdersQueue.Sum(o => o.ExpectedPrintDuration.Ticks))
                            .ThenBy(m => m.OrdersQueue.Count)
                            .ThenBy(m => m.PrintTime)
                            .First();
                order.MachineId = order.Machine.Id;
                order.ExpectedPrintDuration = order.Machine.PrintTime * order.Quantity * order.Article.Length / order.Machine.MaterialPerPrint;

                SetExpectedPrintDate(order);

                order.Machine.OrdersQueue.Add(order);
            }

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Set expected print date according print time and other orders
        /// </summary>
        /// <param name="order">The order</param>
        private void SetExpectedPrintDate(Order order)
        {
            var lastOrderInMachine = order.Machine!.OrdersQueue.LastOrDefault();

            if (lastOrderInMachine == null
                || DateTime.UtcNow.Hour >= 18
                || lastOrderInMachine.ExpectedPrintDate < DateTime.UtcNow.Date
                || lastOrderInMachine.ExpectedPrintDuration > TimeSpan.FromHours(10d))
            {
                order.ExpectedPrintDate = DateTime.UtcNow.Date;

                if ((DateTime.UtcNow.TimeOfDay + order.ExpectedPrintDuration) > TimeSpan.FromHours(18d))
                {
                    order.ExpectedPrintDate = order.ExpectedPrintDate.AddDays(1d);

                    if (order.ExpectedPrintDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        order.ExpectedPrintDate = order.ExpectedPrintDate.AddDays(2d);
                    }
                    else if (order.ExpectedPrintDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        order.ExpectedPrintDate = order.ExpectedPrintDate.AddDays(1d);
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
                    totalPrintTimeNeeded += queueOrder.ExpectedPrintDuration;
                }

                order.ExpectedPrintDate = lastOrderInMachine.ExpectedPrintDate;

                if (totalPrintTimeNeeded + order.ExpectedPrintDuration > TimeSpan.FromHours(10))
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

            double materialCountNeeded = article.Length * neededOrderArticleQuantity;

            if (material.MeasureUnit == MeasureUnit.m)
            {
                materialCountNeeded /= material.Lenght;
            }

            return (int)Math.Ceiling(materialCountNeeded);
        }


    }
}
