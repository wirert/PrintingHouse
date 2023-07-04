namespace PrintingHouse.Core.Contracts
{
    using PrintingHouse.Core.Models.Employee;

    public interface IEmployeeService
    {
        Task AddAsync(AddEmployeeViewModel model);

        Task<IEnumerable<Guid>> GetAllEmployeesUserIdAsync();
    }
}
