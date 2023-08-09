namespace PrintingHouse.UnitTests
{
    using Moq;

    [TestFixture]
    public class FileServiceTests
    {
        private IMock<IMinIoRepository> repositoryMock;
        private IFileService fileService;
        private Guid validArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e");
        private Guid invalidArticleId = Guid.NewGuid();
        private string validFileName = "1.1_1.jpg";


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var repositoryMock = new Mock<IMinIoRepository>();

            repositoryMock.Setup(r => r.GetFileAsync(validArticleId, validFileName)).Returns(async () => new MemoryStream());
            repositoryMock.Setup(r => r.GetFileAsync(invalidArticleId, "")).Returns(async () => null);

            fileService = new FileService(repositoryMock.Object);
        }

        [Test]
        public async Task GetFileAsyncTest()
        {
            var result = await fileService.GetFileAsync(validArticleId, validFileName);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetFileAsyncThrowsIfNoFileFound()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async ()
                => await fileService.GetFileAsync(invalidArticleId, ""));
        }
    }
}
