namespace PrintingHouse.Core.Services
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Infrastructure.Data.Common.Contracts;
    using Microsoft.AspNetCore.Http;

    public class FileService : IFileService
    {
        private readonly IMinIoRepository repo;

        public FileService(IMinIoRepository _repo)
        {
            repo = _repo;
        }

        public async Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName)
        {
            try
            {
                var result = await repo.GetFileAsync(BucketName, fileName);

                return result;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Can't get the file from Database", e);
            }
        }

        public async Task SaveFileAsync(Guid BucketName, string fileName, IFormFile content)
        {
            await repo.AddFileAsync(BucketName, fileName, content);
        }
    }
}
