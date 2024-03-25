namespace PrintingHouse.UnitTests
{
    [TestFixture]
    public class MaterialServiceTests
    {
        private IRepository repo;
        private PrintingHouseDbContext dbContext;
        private IMaterialService materialService;
       
        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
                .UseInMemoryDatabase("PrintingHouseDb")
                .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            materialService = new MaterialService(repo);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
            repo.Dispose();
        }

        [Test]
        public void GetMaterialByIdThrowsIfIdInvalid()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await materialService.GetMaterialByIdAsync(0), "Material Id is not valid");
        }

        [Test]
        public async Task GetMaterialByIdTest()
        {
            var expectedName = "Plain paper A2";

            var material = await materialService.GetMaterialByIdAsync(1);

            Assert.That(material.Type == expectedName);
        }

        [TestCase(-20)]
        [TestCase(100001)]
        [TestCase(-1)]
        public void AddToStoreHouseThrowsIfQuantityIncorrect(int quantity)
        {
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await materialService.AddToStoreHouseAsync(1, quantity));
        }

        [Test]
        public async Task AddToStoreHouseThrowsIfMaterialNotFound()
        {
            var material = await repo.GetByIdAsync<Material>(1);
            material!.IsActive = false;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await materialService.AddToStoreHouseAsync(material.Id, 20));
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await materialService.AddToStoreHouseAsync(0, 20));
        }

        [Test]
        public async Task AddToStoreHouseThrowsIfMaterialTooMuch()
        {
            var material = await repo.GetByIdAsync<Material>(1);
            material!.InStock = int.MaxValue - 10;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await materialService.AddToStoreHouseAsync(material.Id, 20));
        }

        [Test]
        public async Task AddToStoreHouseTest()
        {
            var materialName = await materialService.AddToStoreHouseAsync(1, 20);

            Assert.That(materialName != null);
        }

        [Test]
        public async Task GetAllMaterialTest()
        {
            var material = await repo.GetByIdAsync<Material>(1);
            material!.IsActive = false;
            await repo.SaveChangesAsync();

            var materials = await materialService.GetAllMaterialsAsync();

            Assert.That(materials != null);
            Assert.That(materials!.Count().Equals(2));
        }

        [Test]
        public void GetMaterialQuantityByIdThrowsIfNoMaterialFound()
        {
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await materialService.GetMaterialQuantityByIdAsync(0));
        }

        [Test]
        public async Task GetMaterialQuantityByIdTest()
        {
            var materialQuantity = await materialService.GetMaterialQuantityByIdAsync(1);

            Assert.That(materialQuantity != null);
        }
    }
}
