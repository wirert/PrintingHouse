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
    using PrintingHouse.Core.Services.Admin;
    using PrintingHouse.Core.Exceptions;

    /// <summary>
    /// Client service
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IRepository repo;
        private readonly IEmployeeService employeeService;

        public ClientService(
            IRepository _repo,
            IEmployeeService _employeeService)
        {
            repo = _repo;
            employeeService = _employeeService;
        }

        /// <summary>
        /// Create new client or restore and update deleted
        /// </summary>
        /// <param name="model">Add client view model with data from form</param>        
        /// <param name="userId">current user id</param>
        /// <returns></returns>
        /// <exception cref="ClientNameExistsException"></exception>
        public async Task AddNewAsync(AddClientViewModel model, Guid userId)
        {
            var client = await repo.All<Client>(c => c.Name == model.Name)
                .FirstOrDefaultAsync();

            bool isNewClient = false;
            if (client != null)
            {
                if (client.IsActive)
                {
                    throw new ClientNameExistsException();
                }

                client.IsActive = true;
            }
            else
            {
                isNewClient = true;
                client = new Client()
                {
                    Name = WebUtility.HtmlEncode(model.Name)
                };
            }

            var merchantId = await employeeService.GetIdByUserIdAsync(userId);

            client.MerchantId = merchantId;
            client.PhoneNumber = WebUtility.HtmlEncode(model.PhoneNumber);
            client.Email = WebUtility.HtmlEncode(model.Email);
            client.MerchantId = model.MerchantId;
            if (isNewClient)
            {
                await repo.AddAsync(client);
            }
            await repo.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAndNameAsync(Guid id, string name)
        {
            var client = await repo
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
