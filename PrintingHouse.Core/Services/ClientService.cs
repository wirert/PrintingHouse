namespace PrintingHouse.Core.Services
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Client;
    using Infrastructure.Data.Common.Contracts;
    using Infrastructure.Data.Entities;

    /// <summary>
    /// Client service
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IRepository repo;

        public ClientService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Create new client
        /// </summary>
        /// <param name="model">Add client view model with data from form</param>
        /// <returns></returns>
        public async Task AddNewAsync(AddClientViewModel model)
        {
            var client = new Client()
            {
                Name = WebUtility.HtmlEncode(model.Name),
                PhoneNumber = WebUtility.HtmlEncode(model.PhoneNumber),
                Email = WebUtility.HtmlEncode(model.Email),
                MerchantId = model.MerchantId
            };

            await repo.AddAsync(client);
            await repo.SaveChangesAsync();
        } 

        /// <summary>
        /// Whether client exist by given name
        /// </summary>
        /// <param name="name">Client name</param>
        /// <returns>Boolean</returns>
        public async Task<bool> ExistByName(string name)
        {
            return await repo
                .AllReadonly<Client>(c => c.Name == name && c.IsActive)
                .AnyAsync();
        }

        public async Task<bool> ExistsByIdAndNameAsync(Guid id, string name)
        {
            var client =  await repo
                .AllReadonly<Client>(c => c.Id == id && c.Name == name && c.IsActive)
                .FirstOrDefaultAsync();

            return client != null;
        }

        /// <summary>
        /// Gets all active clients
        /// </summary>
        /// <returns>Enumeration of All client view model</returns>
        public async Task<IEnumerable<AllClientViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Client>(c => c.IsActive)
                .Include(c => c.Articles)
                .Select(c => new AllClientViewModel()
                {
                    Id = c.Id,

                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    MerchantName = $"{c.Merchant.ApplicationUser.FirstName!} {c.Merchant.ApplicationUser.LastName!}",
                    Articles = c.Articles.Count(a => a.IsActive)
                })
                .ToArrayAsync();
        }        
    }
}
