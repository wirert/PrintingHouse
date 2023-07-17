namespace PrintingHouse.Core.Services.Contracts
{
    using AdminModels.ApplicationUser;

    public interface IAccountService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllNewEmployees(IEnumerable<Guid> employeeUserIds);
    }
}
