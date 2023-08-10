namespace PrintingHouse.UnitTests
{
    using PrintingHouse.Core.Services;

    [TestFixture]
    public class ColorServiceTests
    {
        private IRepository repo;
        private PrintingHouseDbContext dbContext;
        private IColorService colorService;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
                .UseInMemoryDatabase("PrintingHouseDb")
                .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            colorService = new ColorService(repo);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [TestCase(-20)]
        [TestCase(100001)]
        [TestCase(-1)]
        public void AddToStoreHouseThrowsIfQuantityIncorrect(int quantity)
        {
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await colorService.AddToStoreHouseAsync(1, quantity));
        }

        [Test]
        public void AddToStoreHouseThrowsIfMaterialNotFound()
        {
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await colorService.AddToStoreHouseAsync(0, 20));
        }

        [Test]
        public async Task AddToStoreHouseThrowsIfQuantityTooMuch()
        {
            var color = await repo.GetByIdAsync<Color>(1);
            color!.InStock = int.MaxValue - 10;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await colorService.AddToStoreHouseAsync(color.Id, 20));
        }

        [Test]
        public async Task AddToStoreHouseTest()
        {
            var colorName = await colorService.AddToStoreHouseAsync(1, 20);

            Assert.NotNull(colorName);
        }

        [Test]
        public async Task GetAllTest()
        {   
            var materials = await colorService.GetAllAsync();

            Assert.NotNull(materials);
            Assert.IsTrue(materials.Count().Equals(7));
        }

        [Test]
        public async Task GetAllByColorModelIdTest()
        {
            var materials = await colorService.GetAllByColorModelIdAsync(1);

            Assert.NotNull(materials);
            Assert.IsTrue(materials.Count().Equals(3));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
