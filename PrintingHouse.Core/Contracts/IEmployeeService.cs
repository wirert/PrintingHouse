namespace PrintingHouse.Core.Contracts
{
    using AdminModels.Employee;

    public interface IEmployeeService
    {
        Task AddAsync(AddEmployeeViewModel model);

        Task<IEnumerable<Guid>> GetAllEmployeesUserIdAsync();

        Task<IEnumerable<AllEmployeeViewModel>> GetAllAsync();

        Task<EditEmployeeViewModel> GetByIdAsync(int id);

        Task ChnagePositionAsync(EditEmployeeViewModel model);

        Task DeleteAsync(int id);

        Task<int> GetIdByUserIdAsync(Guid userId);
    }
}
