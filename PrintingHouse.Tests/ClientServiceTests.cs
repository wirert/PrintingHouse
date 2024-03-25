namespace PrintingHouse.UnitTests
{
    using Core.Exceptions;
    using Core.Models.Client;
    using Core.Models.Order;

    [TestFixture]
    public class ClientServiceTests
    {
        private IRepository repo;
        private PrintingHouseDbContext dbContext;
        private IClientService clientService;
        private Guid userId;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
                .UseInMemoryDatabase("PrintingHouseDb")
                .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            clientService = new ClientService(repo);
        }

        [OneTimeTearDown] 
        public void OneTimeTearDown()
        {
            dbContext.Dispose();
            repo.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            userId = Guid.Parse("e7065dbb-0c70-48da-902c-9f6f2536c505");

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [Test]
        public void AddThrowsIfClientNameAlreadyExistAndIsActive()
        {
            string existingName = "Test Client";
            var newClient = new AddClientViewModel() { Name = existingName, PhoneNumber = "0877666", Email = "test@test.com" };

            Assert.ThrowsAsync<ClientNameExistsException>(async () => await clientService.AddNewAsync(newClient, userId));
        }

        [Test]
        public async Task AddNewTest()
        {
            var newClient = new AddClientViewModel() { Name = "New client", PhoneNumber = "0877666", Email = "test@test.com" };

            await clientService.AddNewAsync(newClient, userId);

            var clientsCount = await repo.AllReadonly<Client>().CountAsync();

            Assert.That(clientsCount, Is.EqualTo(4));
        }

        [Test]
        public async Task AddWhenNameExistsAndIsNotActiveShouldActivateAndUpdate()
        {
            var newClient = new AddClientViewModel() { Name = "New client", PhoneNumber = "0877666", Email = "test@test.com" };

            await clientService.AddNewAsync(newClient, userId);

            var clientToDelete = await repo.All<Client>(c => c.Name == "New client").FirstAsync();
            var deletedClientId = clientToDelete.Id;

            clientToDelete.IsActive = false;
            await repo.SaveChangesAsync();

            newClient = new AddClientViewModel() { Name = "New client", PhoneNumber = "01111111", Email = "new@new.com" };

            await clientService.AddNewAsync(newClient, userId);

            var client = await repo.All<Client>(c => c.Name == "New client").FirstAsync();

            Assert.That(client.Id, Is.EqualTo(deletedClientId));
            Assert.That(client.PhoneNumber, Is.EqualTo(newClient.PhoneNumber));
            Assert.That(client.Email, Is.EqualTo(newClient.Email));
        }

        [Test]
        public async Task DeleteThrowsIfIdNotExistOrAlreadyDeleted()
        {
            var clientId = Guid.Parse("46b7d975-1579-4dad-bdc6-f9dbd0232eab");

            var client = await repo.GetByIdAsync<Client>(clientId);
            client!.IsActive = false;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await clientService.DeleteAsync(clientId));

            clientId = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentException>(async () => await clientService.DeleteAsync(clientId));

        }

        [Test]
        public async Task DeleteThrowsIfAnyActiveClientOrder()
        {
            var orderService = new OrderService(repo);
            var clientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6");
            var order = new AddOrderViewModel()
            {
                ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                ArticleName = "Vinil Article",
                Quantity = 10
            };
            await orderService.CreateOrderAsync(order);

            Assert.ThrowsAsync<DeleteClientException>(async () => await clientService.DeleteAsync(clientId));
        }

        [Test]
        public async Task DeleteTest()
        {
            var clientId = Guid.Parse("46b7d975-1579-4dad-bdc6-f9dbd0232eab");
            var article = new Article()
            {
                Id = Guid.Parse("dac79be6-cffa-4d6b-b34d-0fc05c188f6f"),
                Name = "Test Delete",
                ClientId = clientId,
                MaterialId = 2,
                ColorModelId = 2,
                Length = 4.5,
                ArticleNumber = "",
                ImageName = ""
            };
            await repo.AddAsync(article);
            await repo.SaveChangesAsync();
            await clientService.DeleteAsync(clientId);
            await repo.SaveChangesAsync();
            var deletedClient = await repo.GetByIdAsync<Client>(clientId);

            Assert.That(deletedClient != null);
            Assert.That(deletedClient!.IsActive == false);

            var deletedArticle = await repo.GetByIdAsync<Article>(article.Id);

            Assert.That(deletedArticle!.IsActive == false);
        }

        [Test]
        public async Task ExistsByIdAndNameTest()
        {
            var validClientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6");
            var validName = "Test Client";
            var isExist = await clientService.ExistsByIdAndNameAsync(validClientId, validName);

            Assert.That(isExist);

            isExist = await clientService.ExistsByIdAndNameAsync(validClientId, "Wrong name");

            Assert.That(!isExist);
        }

        [Test]
        public async Task GetAllTest()
        {
            var allClients = await clientService.GetAllAsync();

            Assert.That(allClients.Count(), Is.EqualTo(3));
        }
    }
}
