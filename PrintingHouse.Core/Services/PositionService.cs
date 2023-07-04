namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Position;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    public class PositionService : IPositionService
    {
        private readonly IRepository repo;

        public PositionService(IRepository _repo)
        {
            repo = _repo;
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
