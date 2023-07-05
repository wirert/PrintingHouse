namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using AdminModels.Employee;
    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repo;

        public EmployeeService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddAsync(AddEmployeeViewModel model)
        {
            var employee = new Employee()
            {
                ApplicationUserId = model.ApplicationUserId,
                PositionId = model.PositionId
            };

            await repo.AddAsync(employee);
            await repo.SaveChangesAsync();
        }

        public async Task EditAsync(EditEmployeeViewModel model)
        {
            var employee = await repo.GetByIdAsync<Employee>(model.Id);

            employee.PositionId = model.PositionId;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllEmployeeViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Employee>(e => e.IsActive)
                .Select(e => new AllEmployeeViewModel
                {
                    Id = e.ApplicationUserId.ToString(),
                    FullName = $"{e.ApplicationUser.FirstName} {e.ApplicationUser.LastName}",
                    PhoneNumber = e.ApplicationUser.PhoneNumber,
                    EmployeeNumber = e.Id,
                    Position = e.Position.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetAllEmployeesUserIdAsync()
        {
            return await repo.AllReadonly<Employee>()
                .Select(e => e.ApplicationUserId)
                .ToArrayAsync();
        }

        public async Task<EditEmployeeViewModel?> GetByIdAsync(int id)
        {
            var employee = await repo.GetByIdAsync<Employee>(id);

            if (employee == null)
            {
                return null;
            }

            return new EditEmployeeViewModel()
            {
                Id = employee.Id,
                ApplicationUserId = employee.ApplicationUserId,
                PositionId = employee.PositionId                
            };
                
        }
    }
}
