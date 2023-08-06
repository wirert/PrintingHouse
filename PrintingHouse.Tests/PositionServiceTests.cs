namespace PrintingHouse.UnitTests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using PrintingHouse.Core.AdminModels.Position;
    using PrintingHouse.Core.Services.Admin;
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data;
    using PrintingHouse.Infrastructure.Data.Common;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities;

    public class PositionServiceTests
    {
        private IRepository repo;
        private Mock<IRepository> repoMock;
        private IPositionService positionService;
        private PrintingHouseDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
                .UseInMemoryDatabase("PrintingHouseDb")
                .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repo = new Repository(dbContext);

            positionService = new PositionService(repo);
        }

        [Test]
        public async Task GetAllReturnsAllActivePositions()
        {
            IEnumerable<PositionViewModel> result = await positionService.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(6));

            var entities = new List<Position>()
            {
                new Position() { Name = "testafda2", IsActive = false }
            };

            await repo.AddRangeAsync(entities);
            await repo.SaveChangesAsync();

            positionService = new PositionService(repo);

            result = await positionService.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(6));
        }

        [Test]
        public void DeleteThrowsIfNotExist()
        {
            var invalidId = 1111111;

            Assert.CatchAsync<ArgumentException>(async () => await positionService.DeleteAsync(invalidId), "There is no such position!");
        }

        [Test]
        public void DeleteThrowsIfAnyEmployeeWithThisPosition()
        {
            Assert.CatchAsync<InvalidOperationException>(async () => await positionService.DeleteAsync(1), "There are employees on this position!");
        }

        [Test]
        public async Task AddAndDeleteTest()
        {
            var position = new AddPositionViewModel() { Name = "new position" };

            await positionService.AddNewAsync(position);

            var allActivePositions = await positionService.GetAllAsync();

            Assert.That(allActivePositions.Count(), Is.EqualTo(7));

            await positionService.DeleteAsync(7);

            allActivePositions = await positionService.GetAllAsync();

            Assert.That(allActivePositions.Count(), Is.EqualTo(6));
        }

        [Test]
        public void AddThrowsIfPositionExists()
        {
            var position = new AddPositionViewModel() { Name = "Manager" };

            Assert.CatchAsync<ArgumentException>(async () => await positionService.AddNewAsync(position), $"Position {position.Name} already exist!");
        }

        [Test]
        public async Task AddActivateDeletedPosition()
        {
            await positionService.DeleteAsync(4);

            var newPosition = new AddPositionViewModel() { Name = "Designer" };

            await positionService.AddNewAsync(newPosition);

            var position = await repo.GetByIdAsync<Position>(4);

            Assert.That(position.Name, Is.EqualTo(newPosition.Name));
        }
    }
}