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

            Assert.That(clientsCount, Is.EqualTo(3));
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
            var clientId = userId;

            Assert.ThrowsAsync<ArgumentException>(async () => await clientService.DeleteAsync(clientId));

            clientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6");

            await clientService.DeleteAsync(clientId);

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
        public async Task ExistsByIdAndNameTest()
        {
            var validClientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6");
            var validName = "Test Client";
            var isExist = await clientService.ExistsByIdAndNameAsync(validClientId, validName);

            Assert.True(isExist);

            isExist = await clientService.ExistsByIdAndNameAsync(validClientId, "Wrong name");

            Assert.False(isExist);
        }

        [Test]
        public async Task GetAllTest()
        {
            var allClients = await clientService.GetAllAsync();

            Assert.That(allClients.Count(), Is.EqualTo(2));
        }
    }
}
