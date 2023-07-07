namespace PrintingHouse.Core.Contracts
{
    using AdminModels.Employee;

    public interface IEmployeeService
    {
        Task AddAsync(AddEmployeeViewModel model);

        Task<IEnumerable<Guid>> GetAllEmployeesUserIdAsync();

        Task<IEnumerable<AllEmployeeViewModel>> GetAllAsync();

        Task<EditEmployeeViewModel> GetByIdAsync(int id);

        Task EditAsync(EditEmployeeViewModel model);

        Task DeleteAsync(int id);
    }
}
