namespace PrintingHouse.Infrastructure.Data.Common.Contracts
{
    public interface IMinIoRepository
    {
        Task AddFileAsync(Guid BucketName, string fileName, byte[] content);

        Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName);
    }
}
