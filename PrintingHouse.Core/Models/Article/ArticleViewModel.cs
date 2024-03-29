﻿namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static Infrastructure.Constants.DataConstants;
    using Infrastructure.Data.Entities.Enums;

    public class ArticleViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(Article.MaxNameLength, MinimumLength = Article.MinNameLength)]
        public string Name { get; set; } = null!;

        public IFormFile? DesignFile { get; set; }

        [Required]
        public int MaterialId { get; set; }

        public string MaterialName { get; set; } = null!;

        public MeasureUnit MeasureUnit { get; set; }

        [Required]
        public int ColorModelId { get; set; }

        [Required]
        [Range(0, Article.MaxLength)]
        public double Length { get; set; } = 1;

        [Required]        
        public Guid ClientId { get; set; }

        public int ClientNumber { get; set; }

        public string ClientName { get; set; } = null!;

        public IList<AddArticleColorVeiwModel> Colors { get; set; } = new List<AddArticleColorVeiwModel>();
    }
}
