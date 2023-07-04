using PrintingHouse.Core.Models.Account;
using PrintingHouse.Core.Models.Employee;

namespace PrintingHouse.Core.Contracts
{
    public interface IAccountService
    {
        Task RegisterEmployee(RegisterViewModel model);

        Task<IEnumerable<AllUsersViewModel>> GetAllNewEmployees(IEnumerable<Guid> employeeUserIds);
        
    }
}
