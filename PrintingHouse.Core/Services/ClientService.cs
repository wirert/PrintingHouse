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
    using Infrastructure.Data.Entities.Enums;
    using PrintingHouse.Core.Exceptions;

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

            var merchantId = await repo.AllReadonly<Employee>(e => e.ApplicationUserId == userId)
                                    .Select(e => e.Id)
                                    .SingleAsync();

            client.MerchantId = merchantId;
            client.PhoneNumber = WebUtility.HtmlEncode(model.PhoneNumber);
            client.Email = WebUtility.HtmlEncode(model.Email);
            if (isNewClient)
            {
                await repo.AddAsync(client);
            }
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Soft delete Client with his articles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DeleteClientException"></exception>
        public async Task DeleteAsync(Guid id)
        {
            var client = await repo.All<Client>(c => c.Id == id)
                .Include(c => c.Articles)
                .FirstOrDefaultAsync();

            if (client == null || client.IsActive == false) 
            {
                throw new ArgumentException("Client id is altered");
            }

            var anyActiveClientOrders = await repo.AllReadonly<Order>(o => o.Article.ClientId == id)
                                    .Where(o => o.Status != OrderStatus.Completed &&
                                                o.Status != OrderStatus.Canceled)
                                    .AnyAsync();
            if (anyActiveClientOrders)
            {
                throw new DeleteClientException();
            }

            foreach (var article in client.Articles) 
            {
                article.IsActive = false;
            }

            client.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Check whether Client with given id and name exists
        /// </summary>
        /// <param name="id">client id</param>
        /// <param name="name">client name</param>
        /// <returns>Boolean</returns>
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
