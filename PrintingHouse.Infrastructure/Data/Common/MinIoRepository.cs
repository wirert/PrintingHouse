namespace PrintingHouse.Infrastructure.Data.Common
{
    using Contracts;
    using Minio;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class MinIoRepository : IMinIoRepository
    {
       public readonly IMinioClient minioClient;

        public MinIoRepository(IMinioClient _minioClient)
        {
            minioClient = _minioClient;
        }

        public async Task AddFileAsync(Guid BucketName, string fileName, byte[] content)
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(BucketName.ToString());
            if (await minioClient.BucketExistsAsync(bucketExistArgs) == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(BucketName.ToString());

                await minioClient.MakeBucketAsync(makeBucketArgs);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(BucketName.ToString())
                .WithObject(fileName)
                .WithContentType("application/octet-stream")
                .WithStreamData(new MemoryStream(content));

            await minioClient.PutObjectAsync(putObjectArgs);
        }

        public async Task<MemoryStream> GetFileAsync(Guid BucketName, string fileName)
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(BucketName.ToString());
            if (await minioClient.BucketExistsAsync(bucketExistArgs) == false)
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
    }
}
