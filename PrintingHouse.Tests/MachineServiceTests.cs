namespace PrintingHouse.UnitTests
{
    using Infrastructure.Data.Entities.Enums;

    [TestFixture]
    public class MachineServiceTests
    {
        private PrintingHouseDbContext dbContext;
        private IRepository repo;
        private IMachineService machineService;

        [SetUp] 
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
               .UseInMemoryDatabase("PrintingHouseDb")
               .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            machineService = new MachineService(repo);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [Test]
        public async Task GetMachinesIdsTest()
        {
            var machines = await machineService.GetMachinesIdsAsync();

            Assert.IsTrue(machines.Any());
            Assert.IsTrue(machines.Count().Equals(5));

            var scrapMachine = await repo.All<Machine>().FirstAsync();
            scrapMachine.Status = MachineStatus.Scrapped;
            await repo.SaveChangesAsync();

            machines = await machineService.GetMachinesIdsAsync();

            Assert.IsTrue(machines.Count().Equals(4));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(1000)]
        [TestCase(-20)]
        public async Task GetMachineOrdersThrowsIfMachineIdIsInvalid(int machineId)
        {
            var scrapMachine = await repo.All<Machine>(m => m.Id == 1).FirstAsync();
            scrapMachine.Status = MachineStatus.Scrapped;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await machineService.GetMachineOrdersAsync(machineId), "Machine id is altered");
        }

        [Test]
        public async Task GetMachineOrdersTest()
        {
            var machineOrdersModel = await machineService.GetMachineOrdersAsync(3);

            Assert.IsTrue(machineOrdersModel.Orders.Count().Equals(2));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
