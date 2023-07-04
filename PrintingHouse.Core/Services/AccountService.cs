using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintingHouse.Core.Contracts;
using PrintingHouse.Core.Models.Account;
using PrintingHouse.Core.Models.Employee;
using PrintingHouse.Infrastructure.Data.Common.Contracts;
using PrintingHouse.Infrastructure.Data.Entities.Account;

namespace PrintingHouse.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(
            IRepository _repo,
            UserManager<ApplicationUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllNewEmployees(IEnumerable<Guid> employeeUserIds)
        {
            return await userManager.Users
                    .Where(u => u.IsActive &&
                            employeeUserIds.Contains(u.Id) == false)
                    .Select(u => new AllUsersViewModel()
                    {
                        Id = u.Id,
                        FullName = $"{u.FirstName} {u.LastName}"
                    })
                    .ToListAsync();
        }

        public Task RegisterEmployee(RegisterViewModel model)
        {
            
            throw new NotImplementedException();
        }
    }
}
