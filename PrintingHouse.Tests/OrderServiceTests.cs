namespace PrintingHouse.UnitTests
{
    using Core.Models.Order;
    using Infrastructure.Data.Entities.Enums;
    using PrintingHouse.Core.Exceptions;
    using PrintingHouse.Core.Services;

    [TestFixture]
    public class OrderServiceTests
    {
        private readonly Guid validArticleId = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358");
        private readonly Guid notExistingGuid = Guid.Parse("fd2191f1-21f5-43bf-8266-06ae5f45033a");
        private readonly Guid validOrderId = Guid.Parse("54b5df45-d161-45c6-a6ba-f0c37d5667b6");
        private Article article;
        private AddOrderViewModel addOrderModel;

        private PrintingHouseDbContext dbContext;
        private IRepository repo;
        private IOrderService orderService;              

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
               .UseInMemoryDatabase("PrintingHouseDb")
               .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);
            article = new Article()
            {
                Id = Guid.Parse("cb3db3d6-d26b-4ab6-95c2-21f765e2c815"),
                Name = "Test vinil test",
                ClientId = Guid.Parse("cb76cf2f-c998-459a-83aa-46035256deea"),
                MaterialId = 2,
                ColorModelId = 2,
                Length = 3,
                ArticleNumber = "2.4",
                ImageName = "2.2_1.jpg"
            };

            addOrderModel = new AddOrderViewModel()
            {
                ArticleId = validArticleId,
                ArticleName = "Movie poster A2",
                Quantity = 10
            };

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
        public async Task GetAllOrdersTest()
        {
            var result = await orderService.GetAllOrdersAsync();

            Assert.That(result != null);
            Assert.That(result!.Count().Equals(6));
        }

        [Test]
        public async Task CreateAddModelByArticleIdThrowsIfArticleIdNotValid()
        {
            Assert.ThrowsAsync<ArgumentException>(async () 
                => await orderService.CreateAddModelByArticleIdAsync(notExistingGuid),
                "Article id is altered");

            article.IsActive = false;
            await repo.AddAsync(article);
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () 
                => await orderService.CreateAddModelByArticleIdAsync(article.Id),
                "Article id is altered");
        }

        [Test]
        public async Task CreateAddModelByArticleIdTest()
        {
            var result = await orderService.CreateAddModelByArticleIdAsync(validArticleId);

            Assert.That(result != null);
            Assert.That(result!.ArticleId.Equals(validArticleId));
        }

        [Test]
        public void CreateOrderThrowsIfArticleIdOrNameInvalid()
        {
            addOrderModel.ArticleName = "Invalid name";
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await orderService.CreateOrderAsync(addOrderModel),
                "Article Id or Name are altered");

            addOrderModel.ArticleId = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await orderService.CreateOrderAsync(addOrderModel), 
                "Article Id or Name are altered");
        }

        [Test]
        public async Task CreateOrderThrowsIfNoMachineAvailable()
        {
            var machine = await repo.GetByIdAsync<Machine>(3);
            machine!.Status = MachineStatus.Broken;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<OrderMachineException>(async ()
                => await orderService.CreateOrderAsync(addOrderModel),
                "No machine availabe!");
        }

        [Test]
        public void ChangeStatusThrowsIfInvalidOrderId()
        {
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await orderService.ChangeStatusAsync(notExistingGuid, OrderStatus.Printing),
                "Order id is altered");
        }

        [TestCase(OrderStatus.Printing, OrderStatus.Waiting)]
        [TestCase(OrderStatus.Printing, OrderStatus.NoConsumable)]
        [TestCase(OrderStatus.NoConsumable, OrderStatus.Completed)]
        [TestCase(OrderStatus.NoConsumable, OrderStatus.Printing)]
        [TestCase(OrderStatus.Waiting, OrderStatus.Completed)]
        [TestCase(OrderStatus.Canceled, OrderStatus.Printing)]
        [TestCase(OrderStatus.Canceled, OrderStatus.NoConsumable)]
        [TestCase(OrderStatus.Canceled, OrderStatus.Waiting)]
        [TestCase(OrderStatus.Canceled, OrderStatus.Completed)]
        [TestCase(OrderStatus.Canceled, OrderStatus.Canceled)]
        [TestCase(OrderStatus.Completed, OrderStatus.Canceled)]
        [TestCase(OrderStatus.Completed, OrderStatus.Completed)]
        [TestCase(OrderStatus.Completed, OrderStatus.NoConsumable)]
        [TestCase(OrderStatus.Completed, OrderStatus.Waiting)]
        [TestCase(OrderStatus.Completed, OrderStatus.Printing)]
        public async Task ChangeStatusThrowsWhenTryInproperStatusChange(OrderStatus orderStatus, OrderStatus wrongStatus)
        {
            var order = await repo.GetByIdAsync<Order>(validOrderId);
            
            order!.Status = orderStatus;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<StatusException>(async ()
                => await orderService.ChangeStatusAsync(validOrderId, wrongStatus));
        }

        [Test]
        public async Task ChangeStatusThrowsIfOrderHaveNoMachineId()
        {
            var order = await repo.GetByIdAsync<Order>(validOrderId);
            order!.MachineId = null;

            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await orderService.ChangeStatusAsync(validOrderId, OrderStatus.Printing),
                 "The machine is buzy! Wait to finish current print.");
        }

        [Test]
        public async Task ChangeStatusThrowsIfTryToStartPrintToBuzyMachine()
        {
            var order = await repo.GetByIdAsync<Order>(Guid.Parse("5a167a8c-5136-4c74-90b2-2c958ae54b20"));
            order!.Status = OrderStatus.Printing;
           
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<StatusException>(async ()
                => await orderService.ChangeStatusAsync(validOrderId, OrderStatus.Printing),
                 "The machine is buzy! Wait to finish current print.");
        }

        [Test]
        public async Task ChangeStatusThrowsIfNoAvailableMaterialWhenTryStatusWaiting()
        {
            var order = await repo.GetByIdAsync<Order>(validOrderId);
            order!.Status = OrderStatus.NoConsumable;
            var material = await repo.GetByIdAsync<Material>(1);
            material!.InStock = 0;

            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<StatusException>(async ()
                => await orderService.ChangeStatusAsync(validOrderId, OrderStatus.Waiting),
                "There is no enough consumables!");
        }

        [Test]
        public async Task ChangeStatusThrowsIfNoAvailableColorsWhenTryStatusWaiting()
        {
            var order = await repo.GetByIdAsync<Order>(validOrderId);
            order!.Status = OrderStatus.NoConsumable;
            var color = await repo.GetByIdAsync<Color>(1);
            color!.InStock = 0;

            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<StatusException>(async ()
                => await orderService.ChangeStatusAsync(validOrderId, OrderStatus.Waiting),
                "There is no enough consumables!");
        }

        [TestCase(OrderStatus.Printing, OrderStatus.Completed)]
        [TestCase(OrderStatus.Printing, OrderStatus.Canceled)]
        [TestCase(OrderStatus.Printing, OrderStatus.Printing)]
        [TestCase(OrderStatus.NoConsumable, OrderStatus.Waiting)]
        [TestCase(OrderStatus.NoConsumable, OrderStatus.Canceled)]
        [TestCase(OrderStatus.NoConsumable, OrderStatus.NoConsumable)]
        [TestCase(OrderStatus.Waiting, OrderStatus.Printing)]
        [TestCase(OrderStatus.Waiting, OrderStatus.Canceled)]
        [TestCase(OrderStatus.Waiting, OrderStatus.Waiting)]
        public async Task ChangeStatusTest(OrderStatus orderStatus, OrderStatus newStatus)
        {
            var order = await repo.GetByIdAsync<Order>(validOrderId);

            order!.Status = orderStatus;
            await repo.SaveChangesAsync();

            await orderService.ChangeStatusAsync(validOrderId, newStatus);

            order = await repo.GetByIdAsync<Order>(validOrderId);

            Assert.That(order!.Status.Equals(newStatus));
        }

        [Test]
        public async Task RearangeAllOrderOfParticularTypeTest()
        {
            var colorModelId = 1;
            var materialId = 1;
            var orderInFrontId = Guid.Parse("5a167a8c-5136-4c74-90b2-2c958ae54b20");

            await orderService.RearangeAllOrderOfParticularTypeAsync(materialId, colorModelId, orderInFrontId);

            var firstOrder = await repo.AllReadonly<Order>(o => o.MachineId == 3 && o.MachinePrintOrderNumber == 1)
                                   .FirstAsync();
            Assert.That(firstOrder.Id.Equals(orderInFrontId));
        }

        [TestCase("2023-08-12")]
        [TestCase("2023-08-11")]
        public async Task RearangeAllOrderOfParticularTypeTestWithDateChange(string date)
        {
            var firstOrder = await repo.GetByIdAsync<Order>(validOrderId);
            
            firstOrder!.ExpectedPrintDate = DateTime.Parse(date);
            firstOrder.ExpectedPrintDuration = TimeSpan.FromHours(11);
            await repo.SaveChangesAsync();

            await orderService.ChangeStatusAsync(firstOrder.Id, OrderStatus.Printing);

            var colorModelId = 1;
            var materialId = 1;
            var orderInFrontId = Guid.Parse("5a167a8c-5136-4c74-90b2-2c958ae54b20");

            await orderService.RearangeAllOrderOfParticularTypeAsync(materialId, colorModelId, orderInFrontId);

            firstOrder = await repo.AllReadonly<Order>(o => o.MachineId == 3 && o.MachinePrintOrderNumber == 1)
                                    .FirstAsync();
            Assert.That(firstOrder.Id.Equals(orderInFrontId));
        }

        [Test]
        public void MoveOrderInFrontThrowsIfNoValidOrderId()
        {
            var invalidId = Guid.NewGuid();

            Assert.ThrowsAsync<OrderChangePositionException>(async ()
                => await orderService.MoveOrderInFrontAsync(invalidId, OrderStatus.Waiting));
        }

        [TestCase(OrderStatus.Printing)]
        [TestCase(OrderStatus.Completed)]
        [TestCase(OrderStatus.Canceled)]
        public void MoveOrderInFrontThrowsIfNoValidStatus(OrderStatus incorrectStatus)
        {
            Assert.ThrowsAsync<OrderChangePositionException>(async ()
                => await orderService.MoveOrderInFrontAsync(validOrderId, incorrectStatus));
        }

        [TestCase(OrderStatus.Printing)]
        [TestCase(OrderStatus.Canceled)]
        [TestCase(OrderStatus.Completed)]        
        public async Task MoveUpOnePositionInQueueThrowsIfStatusNotWaitingOrNoConsumables(OrderStatus incorrectOrderStatus)
        {
            var order = await repo.GetByIdAsync<Order>(validOrderId);

            order!.Status = incorrectOrderStatus;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<OrderChangePositionException>(async ()
                => await orderService.MoveUpOnePositionInQueueAsync(validOrderId),
                "Status of order is different of 'Waiting' or 'NoConsumable'");
        }

        [Test]
        public void MoveUpOnePositionInQueueThrowsIfOrderIdInvalid()
        {
            Assert.ThrowsAsync<OrderChangePositionException>(async ()
                => await orderService.MoveUpOnePositionInQueueAsync(notExistingGuid),
                "Order Id is altered");
        }

        [Test]
        public void MoveUpOnePositionInQueueThrowsIfOrderNumberLessThan2()
        {
            Assert.ThrowsAsync<OrderChangePositionException>(async ()
                => await orderService.MoveUpOnePositionInQueueAsync(validOrderId),
                "Order can't move up");
        }

        [Test]
        public async Task MoveUpOnePositionInQueueThrowsIfNoPriviousNumberFound()
        {
            var order = await repo.GetByIdAsync<Order>(validOrderId);

            order!.MachinePrintOrderNumber = 5;
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<OrderChangePositionException>(async ()
                => await orderService.MoveUpOnePositionInQueueAsync(validOrderId),
                "No privious order number found");
        }

        [Test]
        public async Task MoveUpOnePositionInQueueTest()
        {
            var orderToMoveUpId = Guid.Parse("5a167a8c-5136-4c74-90b2-2c958ae54b20");

            await orderService.MoveUpOnePositionInQueueAsync(orderToMoveUpId);
        }

    }
}
