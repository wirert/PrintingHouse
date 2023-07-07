namespace PrintingHouse.Core.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using AdminModels.ApplicationUser;
    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities.Account;

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
    }
}
