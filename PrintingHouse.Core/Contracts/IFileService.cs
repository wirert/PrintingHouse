namespace PrintingHouse.Core.Contracts
{
    public interface IFileService
    {
        Task SaveFileAsync(Guid BucketName, string fileName, byte[] content);

        Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName);
    }
}
