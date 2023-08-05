namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Machine;
    using Models.Order;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Infrastructure.Data.Entities.Enums;

    public class MachineService : IMachineService
    {
        private readonly IRepository repo;
        private readonly IOrderService orderService;

        public MachineService(IRepository _repo, IOrderService _orderService)
        {
            repo = _repo;
            orderService = _orderService;
        }

        /// <summary>
        /// Gets orders for a particular machine
        /// </summary>
        /// <param name="machineId">machine identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<MachineOrderViewModel> GetMachineOrdersAsync(int machineId)
        {
            var result =  await repo
                .AllReadonly<Machine>(m => m.Id == machineId && m.Status != MachineStatus.Scrapped)
                .Select(m => new MachineOrderViewModel()
                {
                    Id = machineId,
                    Name = m.Name,
                    PrintTime = m.PrintTime,
                    MaterialPerPrint = m.MaterialPerPrint,
                    Model = m.Model,                    
                    Status = m.Status,
                    Orders = m.OrdersQueue.Select(o => new OrderViewModel()
                    {
                        Id = o.Id,
                        Number = o.Number,
                        ArticleName = o.Article.Name,
                        ArticleNo = o.Article.ArticleNumber,
                        ClientName = o.Article.Client.Name,
                        EndDate = o.EndDate,
                        ExpectedPrintDate = o.ExpectedPrintDate,
                        ColorModel = o.Article.MaterialColorModel.ColorModel.Name,
                        Material = o.Article.MaterialColorModel.Material.Type,
                        Width = o.Article.MaterialColorModel.Material.Width,
                        MeasureUnit = o.Article.MaterialColorModel.Material.MeasureUnit,
                        Comment = o.Comment,
                        OrderTime = o.OrderTime,
                        ExpectedPrintTime = o.ExpectedPrintDuration,
                        Status = o.Status,
                        Quantity = o.Quantity
                    })
                    .OrderBy(o => o.Status)
                    .ToList(),
                })
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new ArgumentException("Machine id is altered");
            }

            return result;
        }

        /// <summary>
        /// Gets all machines for select
        /// </summary>
        /// <returns>Machine select view model</returns>
        public async Task<IEnumerable<MachineSelectViewModel>> GetMachinesIdsAsync()
        {
            var machines =  await repo
                .AllReadonly<Machine>(m => m.Status != MachineStatus.Scrapped)
                .Select(m => new MachineSelectViewModel()
                {
                    Id=m.Id,
                    Name = m.Name,
                })
                .ToListAsync();

            machines.Add(new MachineSelectViewModel()
            {
                Id = 0,
                Name = "--Select Machine--"
            });

            return machines;
        }

        /// <summary>
        /// Make order first in queue for printing
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task MoveOrderInFrontAsync(Guid orderId)
        {
           var order =  await repo.AllReadonly<Order>(o => o.Id == orderId)
                .Select(o => new
                {
                    o.Article.MaterialId,
                    o.Article.ColorModelId
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new ArgumentException("Order Id is altered");
            }

            await orderService.RearangeAllOrderOfParticularTypeAsync(order.MaterialId, order.ColorModelId, orderId);
        }
    }
}
