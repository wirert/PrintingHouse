namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;
    using Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Common;


    //Only for testing! Shoud be changed or removed.
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        private readonly IMinIoRepository minIoRepo;

        public ArticleConfiguration(IMinIoRepository _minIoRepo)
        {
            minIoRepo = _minIoRepo;
        }

        public void Configure(EntityTypeBuilder<Article> builder)
        {     
            builder.HasData(CreateArticles());
        }

        private List<Article> CreateArticles()
        {
            var articles = new List<Article>();

            var article = new Article()
            {
                Id = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                Name = "Vinil Article",
                ClientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6"),
                MaterialId = 2,
                ColorModelId = 2,
                Length = 4.5,                
                ArticleNumber = "101.1"                
            };

            article.ImageName = $"{article.ArticleNumber}_1.jpg";

            using (FileStream fs = File.OpenRead(@"DesignPictures\Inquisition Scene 1816.jpg"))
            {
                var file = new FormFile(fs, 0, fs.Length, "Inquisition Scene 1816.jpg", fs.Name);

                minIoRepo.AddFileAsync(Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"), article.ImageName, file );
            }

            articles.Add( article );

            article = new Article()
            {
                Id = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"),
                Name = "Movie poster A2",
                ClientId = Guid.Parse("cb76cf2f-c998-459a-83aa-46035256deea"),
                MaterialId = 1,
                ColorModelId = 1,
                Length = 1,
                ArticleNumber = "102.1"
            };

            article.ImageName = $"{article.ArticleNumber}_1.webp";

            using (FileStream fs = File.OpenRead(@"DesignPictures\movie-poster.webp")) 
            {
               var file = new FormFile(fs, 0, fs.Length, "movie-poster.webp", fs.Name);

               minIoRepo.AddFileAsync(Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"), article.ImageName, file);
            }

            articles.Add(article);

            article = new Article()
            {
                Id = Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"),
                Name = "Salami Teleshki 0.3",
                ClientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6"),
                MaterialId = 3,
                ColorModelId = 2,
                Length = 0.3,
                ArticleNumber = "101.2"
            };

            article.ImageName = $"{article.ArticleNumber}_1.jpg";

            using (FileStream fs = File.OpenRead(@"DesignPictures\teleshki_salam.jpg"))
            {
                var file = new FormFile(fs, 0, fs.Length, "teleshki_salam.jpg", fs.Name);

                minIoRepo.AddFileAsync(Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), article.ImageName, file);
            }

            articles.Add(article);

            return articles;            
        }
    }
}
