namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Client;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    public class ClientService : IClientService
    {
        private readonly IRepository repo;

        public ClientService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddNewAsync(AddClientViewModel model)
        {
            var client = new Client()
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                MerchantId = model.MerchantId
            };

            await repo.AddAsync(client);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> ExistByName(string name)
        {
            return await repo
                .AllReadonly<Client>(c => c.Name == name)
                .AnyAsync();
        }

        public async Task<IEnumerable<AllClientViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Client>(c => c.IsActive)
                .Select(c => new AllClientViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    MerchantName = $"{c.Merchant.ApplicationUser.FirstName!} {c.Merchant.ApplicationUser.LastName!}",
                    Articles = c.Articles.Count
                })
                .ToArrayAsync();
        }
    }
}
