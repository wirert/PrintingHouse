namespace PrintingHouse.UnitTests
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using PrintingHouse.Core.Services.Admin;
    using PrintingHouse.Infrastructure.Data.Entities.Account;

    [TestFixture]
    public class EmployeeServiceTests
    {
        private PrintingHouseDbContext dbContext;
        private IRepository repo;
        private IEmployeeService employeeService;
        private IPositionService positionService;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole<Guid>> roleManager;
        private Mock<IRepository> mockRepository;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
              .UseInMemoryDatabase("PrintingHouseDb")
              .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);

            mockRepository = new Mock<IRepository>();
            mockRepository.Setup(r => r.GetByIdAsync<Employee>(1)).Returns(async () => new Employee());
            mockRepository.Setup(r => r.GetByIdAsync<Employee>(2)).Returns(async () => null);

            positionService = new PositionService(repo);
            userManager = MockHelpers.TestUserManager<ApplicationUser>();
            roleManager = MockHelpers.TestRoleManager<IdentityRole<Guid>>();
            employeeService = new EmployeeService(repo, userManager, roleManager, positionService);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
            repo.Dispose();
            userManager.Dispose();
            roleManager.Dispose();
        }

        [Test]
        public async Task GetAllTest()
        {
            var emp = await employeeService.GetAllAsync();

            Assert.That(emp != null);
        }

        [Test]
        public async Task GetByIdTest()
        {
            employeeService = new EmployeeService(mockRepository.Object, userManager, roleManager, positionService);

            var result = await employeeService.GetByIdAsync(1);
            var nullResult = await employeeService.GetByIdAsync(2);

            Assert.That(result != null);
            Assert.That(nullResult == null);
        }
        
        
    }
}
