namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Entities;

    internal class ArticleColorConfiguration : IEntityTypeConfiguration<ArticleColor>
    {
        public void Configure(EntityTypeBuilder<ArticleColor> builder)
        {
            builder.HasData(CreateArticleColors());
        }

        private List<ArticleColor> CreateArticleColors()
        {
            return new List<ArticleColor>()
            {
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                            ColorId = 4,
                            ColorQuantity = 0.08
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                            ColorId = 5,
                            ColorQuantity = 0.17
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                            ColorId = 6,
                            ColorQuantity = 0.09
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"),
                            ColorId = 7,
                            ColorQuantity = 0.1
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"),
                            ColorId = 1,
                            ColorQuantity = 0.003
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"),
                            ColorId = 2,
                            ColorQuantity = 0.007
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"),
                            ColorId = 3,
                            ColorQuantity = 0.009
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"),
                            ColorId = 4,
                            ColorQuantity = 0.2
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"),
                            ColorId = 5,
                            ColorQuantity = 0.19
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"),
                            ColorId = 6,
                            ColorQuantity = 0.09
                        },
                        new ArticleColor()
                        {
                            ArticleId = Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"),
                            ColorId = 7,
                            ColorQuantity = 0.1
                        }
            };
        }
    }
}
