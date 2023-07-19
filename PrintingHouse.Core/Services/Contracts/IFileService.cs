namespace PrintingHouse.Core.Services.Contracts
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// File service interface for IoC
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Save file mothod for object store
        /// </summary>
        /// <param name="BucketName">Bucket name</param>
        /// <param name="fileName">file name</param>
        /// <param name="content">File content (IFormFile)</param>
        /// <returns></returns>
        Task SaveFileAsync(Guid BucketName, string fileName, IFormFile content);

        /// <summary>
        /// Retrieve file from object store by bucket name and file name
        /// </summary>
        /// <param name="BucketName">Bucket name</param>
        /// <param name="fileName">File name</param>
        /// <returns>Memory stream with file content</returns>
        Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName);
    }
}
