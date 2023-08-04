namespace PrintingHouse.Core.Services
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// File service
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IMinIoRepository repo;

        public FileService(IMinIoRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Retrieve file from MinIO object store by bucket name and file name
        /// </summary>
        /// <param name="BucketName">Bucket name</param>
        /// <param name="fileName">File name</param>
        /// <returns>Memory stream with file content</returns>
        public async Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName)
        {
            var result = await repo.GetFileAsync(BucketName, fileName);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(BucketName), "File not found");
            }

            return result;
        }

        /// <summary>
        /// Save file mothod for MinIO object store
        /// </summary>
        /// <param name="BucketName">Bucket name</param>
        /// <param name="fileName">file name</param>
        /// <param name="content">File content (IFormFile)</param>
        /// <returns></returns>
        public async Task SaveFileAsync(Guid BucketName, string fileName, IFormFile content)
        {
            await repo.AddFileAsync(BucketName, fileName, content);
        }
    }
}
