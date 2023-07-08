namespace PrintingHouse.Core.Services
{
    using System.Threading.Tasks;

    using Contracts;
    using Models.Client;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;

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
    }
}
