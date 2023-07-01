namespace PrintingHouse.Core.Services
{
    using System;
    using System.Threading.Tasks;

    using Minio;

    using Contracts;

    public class FileService : IFileService
    {
        private readonly IMinioClient minio;

        public FileService(IMinioClient _minio)
        {
            minio = _minio;
        }

        public async Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName)
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(BucketName.ToString());
            if (await minio.BucketExistsAsync(bucketExistArgs) == false)
            {
                throw new ArgumentException("There is no such file in store or id is incorrect");
            }

            using var result = new MemoryStream();

            var args = new GetObjectArgs()
                .WithBucket(BucketName.ToString())
                .WithObject(fileName)
                .WithCallbackStream(async stream =>
                {
                   await stream.CopyToAsync(result);
                });

            return result;
        }

        public async Task SaveFileAsync(Guid BucketName, string fileName, byte[] content)
        {

            var bucketExistArgs = new BucketExistsArgs().WithBucket(BucketName.ToString());
            if (await minio.BucketExistsAsync(bucketExistArgs) == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(BucketName.ToString());

                await minio.MakeBucketAsync(makeBucketArgs);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(BucketName.ToString())
                .WithObject(fileName)
                .WithContentType("application/octet-stream")
                .WithStreamData(new MemoryStream(content));

            await minio.PutObjectAsync(putObjectArgs);


        }
    }
}
