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

        public MachineService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<MachineOrderViewModel> GetMachineOrdersAsync(int machineId)
        {
            return await repo
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
                        ExpectedPrintTime = o.ExpectedPrintTime,
                        Status = o.Status,
                        Quantity = o.Quantity
                    })
                    .ToList(),
                })
                .FirstAsync();
        }

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
    }
}
