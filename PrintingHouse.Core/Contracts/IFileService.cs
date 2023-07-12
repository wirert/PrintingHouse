namespace PrintingHouse.Core.Contracts
{
    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        Task SaveFileAsync(Guid BucketName, string fileName, IFormFile content);

        Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName);
    }
}
