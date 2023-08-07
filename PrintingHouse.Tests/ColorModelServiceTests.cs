namespace PrintingHouse.UnitTests
{    
    [TestFixture]
    public class ColorModelServiceTests
    {
        private IRepository repo;
        private PrintingHouseDbContext dbContext;
        private IColorModelService colorModelService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
                .UseInMemoryDatabase("PrintingHouseDb")
                .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            colorModelService = new ColorModelService(repo);
        }

        [SetUp]
        public void SetUp()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [Test]
        public void GetColorModelByMaterialIdThrowsIfIdNotValid()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await colorModelService.GetColorModelByMaterialIdAsync("a"), "Not a number");
        }

        [Test]
        public async Task GetColorModelByMaterialIdTest()
        {
            var result = await colorModelService.GetColorModelByMaterialIdAsync("0");

            Assert.IsTrue(result.Count() == 0);

            result = await colorModelService.GetColorModelByMaterialIdAsync("1");

            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public void GetColorModelColorsThrowsIfIdInvalid()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await colorModelService.GetColorModelColorsAsync(0), "Invalid colorModel Id");
        }

        [Test]
        public async Task GetColorModelColorsTest()
        {
            var result = await colorModelService.GetColorModelColorsAsync(1);

            Assert.IsTrue(result.Count() == 3);
        }
    }
}
