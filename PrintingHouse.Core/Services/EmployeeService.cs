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

        public async Task<IEnumerable<Guid>> GetAllEmployeesUserIdAsync()
        {
            return await repo.AllReadonly<Employee>()
                .Select(e => e.ApplicationUserId)
                .ToArrayAsync();
        }
    }
}
