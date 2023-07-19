namespace PrintingHouse.Core.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using AdminModels.Position;
    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    /// <summary>
    /// Position service
    /// </summary>
    public class PositionService : IPositionService
    {
        private readonly IRepository repo;

        public PositionService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Create new position or restore if non active
        /// </summary>
        /// <param name="viewModel">Add position view model</param>
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

        /// <summary>
        /// Soft delete position
        /// </summary>
        /// <param name="positionId">Positon id</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Throw if there are workers on this position</exception>
        public async Task DeleteAsync(int positionId)
        {
            var position = await repo
                             .All<Position>(p => p.Id == positionId && p.IsActive)
                             .Include(p => p.Employees)
                             .FirstAsync();

            if (position.Employees.Count(e => e.IsActive) > 0)
            {
                throw new InvalidOperationException("There are employees on this position!");
            }

            position.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Get all active positions available
        /// </summary>
        /// <returns>Enumeration of Position view model</returns>
        public async Task<IEnumerable<PositionViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Position>(p => p.IsActive == true)
                .Select(p => new PositionViewModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }
    }
}
