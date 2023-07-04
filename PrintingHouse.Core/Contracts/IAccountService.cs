using PrintingHouse.Core.Models.Account;

namespace PrintingHouse.Core.Contracts
{
    public interface IAccountService
    {
        Task RegisterEmployee(RegisterViewModel model);
        
    }
}
