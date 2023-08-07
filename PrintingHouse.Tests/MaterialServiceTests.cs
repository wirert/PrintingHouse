namespace PrintingHouse.UnitTests
{
    [TestFixture]
    public class MaterialServiceTests
    {
        private IRepository repo;
        private PrintingHouseDbContext dbContext;
        private IMaterialService materialService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
                .UseInMemoryDatabase("PrintingHouseDb")
                .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            materialService = new MaterialService(repo);
        }

        [SetUp]
        public void SetUp()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
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

            Assert.IsTrue(material.Type == expectedName);
        }
    }
}
