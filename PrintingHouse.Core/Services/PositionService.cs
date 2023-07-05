namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using AdminModels.Position;
    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    public class PositionService : IPositionService
    {
        private readonly IRepository repo;

        public PositionService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddNewAsync(AddPositionViewModel viewModel)
        {
            var deletedPositon = await repo.All<Position>(p => p.IsActive == false)
                .FirstOrDefaultAsync(p => p.Name == viewModel.Name);

            if (deletedPositon != null)
            {
                deletedPositon.IsActive = true;
            }
            else
            {
                var position = new Position()
                {
                    Name = viewModel.Name
                };

                await repo.AddAsync(position);
            }
            
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllPositionViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Position>(p => p.IsActive == true)
                .Select(p => new AllPositionViewModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }
    }
}
