namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Employee;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repo;

        public EmployeeService(IRepository _repo)
        {
            repo = _repo;
        }

        public Task AddAsync(AddEmployeeViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Guid>> GetAllEmployeesUserIdAsync()
        {
            return await repo.AllReadonly<Employee>()
                .Select(e => e.ApplicationUserId)
                .ToArrayAsync();
        }
    }
}
