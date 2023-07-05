using PrintingHouse.Core.AdminModels.Employee;
using PrintingHouse.Core.Models.Account;

namespace PrintingHouse.Core.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllNewEmployees(IEnumerable<Guid> employeeUserIds);        
    }
}
