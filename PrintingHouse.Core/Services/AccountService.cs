using PrintingHouse.Core.Contracts;
using PrintingHouse.Core.Models.Account;
using PrintingHouse.Infrastructure.Data.Common.Contracts;

namespace PrintingHouse.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;

        public AccountService(IRepository _repo)
        {
            repo = _repo;
        }

        public Task RegisterEmployee(RegisterViewModel model)
        {
            
            throw new NotImplementedException();
        }
    }
}
