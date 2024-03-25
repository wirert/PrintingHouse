namespace PrintingHouse.UnitTests
{
    using Microsoft.AspNetCore.Http;

    using Moq;

    using PrintingHouse.Core.Models.Article;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;

    [TestFixture]
    public class ArticleServiceTests
    {
        private readonly Guid notExistingGuid = Guid.Parse("fd2191f1-21f5-43bf-8266-06ae5f45033a");
        private readonly Guid validArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e");
        private readonly Guid validClientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6");

        private PrintingHouseDbContext dbContext;
        private IRepository repo;
        private IArticleService articleService;
        private IFileService fileService;
        private IMock<IMinIoRepository> minIoRepoMock;
        private IMaterialColorService materialColorService;
        private IMaterialService materialService;
        private IClientService clientService;
        private IColorModelService colorModelService;

        private ArticleViewModel articleViewModel;



        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<PrintingHouseDbContext>()
                .UseInMemoryDatabase("PrintingHouseDb")
                .Options;
            dbContext = new PrintingHouseDbContext(contextOptions);
            repo = new Repository(dbContext);
            materialColorService = new MaterialColorService(repo);
            materialService = new MaterialService(repo);
            clientService = new ClientService(repo);
            colorModelService = new ColorModelService(repo);
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
            minIoRepoMock = new Mock<IMinIoRepository>();
            fileService = new FileService(minIoRepoMock.Object);

            articleService = new ArticleService(repo, fileService, materialColorService, materialService, clientService, colorModelService);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            articleViewModel = new ArticleViewModel()
            {
                Name = "Test",
                DesignFile = new FormFile(new MemoryStream(), 0, 1000, "name.jpg", "fileName.jpg"),
                ClientId = validClientId,
                ClientName = "Test Client",
                ColorModelId = 1,
                MaterialId = 1,
                Length = 1,
                Colors = new List<AddArticleColorVeiwModel>()
                {
                    new AddArticleColorVeiwModel()
                    {
                        ColorId = 1,
                        ColorQuantity = 0.01
                    },
                    new AddArticleColorVeiwModel()
                    {
                        ColorId = 2,
                        ColorQuantity = 0.01
                    },
                    new AddArticleColorVeiwModel()
                    {
                        ColorId = 3,
                        ColorQuantity = 0.01
                    }
                }
            };
        }

        [Test]
        public async Task ExistByIdTest()
        {
            //fileRepoMock.Setup(f => f.GetFileAsync()).Returns(Task.FromResult(new MemoryStream()));

            Guid realId = validArticleId;

            bool result = await articleService.ExistByIdAsync(realId);

            Assert.That(result == true);

            result = await articleService.ExistByIdAsync(null);

            Assert.That(result == false);

            result = await articleService.ExistByIdAsync(notExistingGuid);

            Assert.That(result == false);
        }

        [Test]
        public async Task DeleteByIdTest()
        {
            var article = new Article()
            {
                Id = Guid.Parse("dac79be6-cffa-4d6b-b34d-0fc05c188f6f"),
                Name = "Test Delete",
                ClientId = validClientId,
                MaterialId = 2,
                ColorModelId = 2,
                Length = 4.5,
                ArticleNumber = "",
                ImageName = ""
            };
            var artId = article.Id;

            await repo.AddAsync(article);
            await repo.SaveChangesAsync();

            await articleService.DeleteByIdAsync(artId);

            var deletedArticle = await repo.GetByIdAsync<Article>(artId);

            Assert.That(deletedArticle!.IsActive == false);

        }

        [Test]
        public void DeleteByIdThrowsIfIdIsAltered()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async ()
                => await articleService.DeleteByIdAsync(notExistingGuid), "Article id is altered");
        }

        [Test]
        public void GetFileNameByIdThrowsIfIdIsAltered()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async ()
                => await articleService.GetFileNameByIdAsync(notExistingGuid), $"Article not found");
        }

        [Test]
        public async Task GetFileNameByIdTest()
        {
            var expectedName = "1.1_1.jpg";
            var id = validArticleId;

            var resultName = await articleService.GetFileNameByIdAsync(id);

            Assert.That(resultName.Equals(expectedName));
        }

        [Test]
        public async Task GetAllTest()
        {
            Guid? clientId = null;
            var allArticles = await articleService.GetAllAsync(clientId);

            Assert.That(allArticles.Count().Equals(3));

            clientId = validClientId;
            var clientArticles = await articleService.GetAllAsync(clientId);

            Assert.That(clientArticles.Count().Equals(2));
        }

        [Test]
        public void CreateThrowsIfClientIdIsNotValid()
        {
            var articleForCreate = articleViewModel;
            articleForCreate.ClientId = notExistingGuid;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.CreateAsync(articleForCreate), "Client name or id is altered");
        }

        [Test]
        public void CreateThrowsIfClientNameIsNotValid()
        {
            var articleForCreate = articleViewModel;
            articleForCreate.ClientName = "Not valid name";

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.CreateAsync(articleForCreate), "Client name or id is altered");
        }

        [Test]
        public void CreateThrowsIfMaterialColorModelNotExist()
        {
            var articleForCreate = articleViewModel;
            articleForCreate.MaterialId = 0;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.CreateAsync(articleForCreate), "Material Id or ColorModel Id is altered");

            articleForCreate.MaterialId = 1;
            articleForCreate.ColorModelId = -20;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.CreateAsync(articleForCreate), "Material Id or ColorModel Id is altered");
        }

        [Test]
        public async Task CreateArticleTest()
        {
            var articleForCreate = articleViewModel;

            await articleService.CreateAsync(articleForCreate);

            var articles = await articleService.GetAllAsync(null);

            Assert.That(articles.Count().Equals(4));
        }

        [Test]
        public void GetSelectVeiwModelWithDataThrowsIfArticleIdIsAltered()
        {
            var clientId = articleViewModel.ClientId;
            var articleId = notExistingGuid;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetSelectVeiwModelWithDataAsync(clientId, articleId), "Article id is altered.");
        }

        [Test]
        public void GetSelectVeiwModelWithDataThrowsIfClientIdIsAltered()
        {
            var clientId = notExistingGuid;
            var articleId = validArticleId;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetSelectVeiwModelWithDataAsync(clientId, articleId), "Client id is altered");
        }

        [Test]
        public async Task GetSelectVeiwModelWithDataTest()
        {
            var clientId = validClientId;
            var articleId = validArticleId;

            var result = await articleService.GetSelectVeiwModelWithDataAsync(clientId, articleId);

            Assert.That(result.ClientId.Equals(clientId));
            Assert.That(result.ArticleId.Equals(articleId));
            Assert.That(result.Materials.Any());
            Assert.That(result.Materials.Count() == 4);
            Assert.That(result.ColorModels.Count() == 1);
        }

        [Test]
        public void EditThrowsIfArticleIdIsInvalid()
        {
            ArticleViewModel invalidArticleViewModel = articleViewModel;
            invalidArticleViewModel.Id = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.EditAsync(invalidArticleViewModel), "Arguments are altered!");
        }

        [Test]
        public void EditThrowsIfArticleClientIdDifferentFromModelClientId()
        {            
            articleViewModel.Id = validArticleId;
            articleViewModel.ClientId = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.EditAsync(articleViewModel), "Arguments are altered!");
        }

        [Test]
        public void EditThrowsIfNotValidMaterialColorModel()
        {
            articleViewModel.Id = validArticleId;
            articleViewModel.MaterialId = -20;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.EditAsync(articleViewModel), "Arguments are altered!");
        }

        [Test]
        public void EditThrowsIfClientIdNotMatchClientName()
        {
            articleViewModel.Id = validArticleId;
            articleViewModel.ClientName = "Invalid name";

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.EditAsync(articleViewModel), "Arguments are altered!");
        }

        [Test]
        public async Task EditArticleTest()
        {
            articleViewModel.Id = validArticleId;

            await articleService.EditAsync(articleViewModel);
            var editedArticle = await repo.GetByIdAsync<Article>(validArticleId);

            Assert.That(editedArticle!.Name.Equals(articleViewModel.Name));

            articleViewModel.DesignFile = null;
            articleViewModel.Name = "Changed name";
            await articleService.EditAsync(articleViewModel);
            editedArticle = await repo.GetByIdAsync<Article>(validArticleId);

            Assert.That(editedArticle!.Name.Equals("Changed name"));
        }

        [TestCase(-20)]
        [TestCase(0)]
        [TestCase(1200)]
        public void GetCreateViewModelWithDataThrowsIfMaterialIdInvalid(int invalidMaterialId)
        {           
            var colorModelId = 1;
            var validClientName = "Test Client";


            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetCreateViewModelWithData(invalidMaterialId, colorModelId, validClientId, validClientName),
                "Input data is altered.");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("   ")]
        public void GetCreateViewModelWithDataThrowsIfNoClientName(string? invalidName)
        {
            var validMaterialId = 1;
            var colorModelId = 1;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetCreateViewModelWithData(validMaterialId, colorModelId, validClientId, invalidName),
                 "Input data is altered.");
        }

        [TestCase(1, 2)]
        [TestCase(3, 1)]
        [TestCase(1, 0)]
        [TestCase(2, -20)]
        public void GetCreateViewModelWithDataThrowsIfNoValidMaterialColorModelFound(int validMaterialId, int colorModelId)
        {
            
            var validClientName = "Test Client";

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetCreateViewModelWithData(validMaterialId, colorModelId, validClientId, validClientName),
                "Input data is altered.");
        }

        [TestCase("Client 2")]
        [TestCase("Invalid name")]
        public void GetCreateViewModelWithDataThrowsIfClientIdAndClientNameNotFromSameEntity(string clientName)
        {
            var validMaterialId = 1;
            var colorModelId = 1;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetCreateViewModelWithData(validMaterialId, colorModelId, validClientId, clientName),
                "Input data is altered.");
        }

        [Test]
        public async Task GetCreateViewModelWithDataTest()
        {
            var materialId = 1;
            var colorModelId = 1;
            var clientName = "Test Client";

           var result = await articleService.GetCreateViewModelWithData(materialId, colorModelId, validClientId, clientName);

            Assert.That(result.Colors.Any());
            Assert.That(result.Colors.Count().Equals(3));
            Assert.That(result.Length.Equals(1));
        }

        [Test]
        public void GetEditViewModelWithDataThrowsIfArticleIdNotValid()
        {
            var materialId = 1;
            var colorModelId = 1;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetEditViewModelWithData(materialId, colorModelId, notExistingGuid),
                "Article id is not valid");
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void GetEditViewModelWithDataThrowsIfNoValidMaterialId(int invalidMaterialId)
        {
            var colorModelId = 2;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetEditViewModelWithData(invalidMaterialId, colorModelId, validArticleId),
                "Invalid material id");
        }

        [TestCase(1, -1)]
        [TestCase(1, 2)]        
        [TestCase(1, 0)]        
        [TestCase(3, 1)]        
        public void GetEditViewModelWithDataThrowsIfNoValidMaterialColorModelFoundWhenMaterialIdAndColorModelIdAreNotNull(int materialId, int invalidColorModelId)
        {
            Assert.ThrowsAsync<ArgumentException>(async ()
                => await articleService.GetEditViewModelWithData(materialId, invalidColorModelId, validArticleId),
                "Input data (materialId or colorModelId) is altered");            
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(1, null)]
        [TestCase(null, 2)]
        [TestCase(null, null)]
        public async Task GetEditViewModelWithDataTest(int? materialId, int? colorModelId)
        {

            var result = await articleService.GetEditViewModelWithData(materialId, colorModelId, validArticleId);

            Assert.That(result != null);
        }
    }
}
