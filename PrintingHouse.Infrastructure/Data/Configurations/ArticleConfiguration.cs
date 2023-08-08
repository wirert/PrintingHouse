namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;


    //Only for testing! Shoud be changed or removed.
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(CreateArticles());
        }

        private List<Article> CreateArticles()
        {
            var articles = new List<Article>()
            {
                new Article()
                {
                    Id = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                    Name = "Vinil Article",
                    ClientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6"),
                    MaterialId = 2,
                    ColorModelId = 2,
                    Length = 4.5,
                    ArticleNumber = "1.1" ,
                    ImageName = "1.1_1.jpg"
                },
                new Article()
                {
                    Id = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"),
                    Name = "Movie poster A2",
                    ClientId = Guid.Parse("cb76cf2f-c998-459a-83aa-46035256deea"),
                    MaterialId = 1,
                    ColorModelId = 1,
                    Length = 1,
                    ArticleNumber = "2.1",
                    ImageName = "2.1_1.webp"
                },
                new Article()
                {
                    Id = Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"),
                    Name = "Salami Teleshki 0.3",
                    ClientId = Guid.Parse("ffbddf06-701d-49f2-8e4b-df760d13b2a6"),
                    MaterialId = 3,
                    ColorModelId = 2,
                    Length = 0.3,
                    ArticleNumber = "1.2",
                    ImageName = "1.2_1.jpg"
                }                
            };

            return articles;
        }
    }
}
