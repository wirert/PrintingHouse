﻿namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    public class AllArticleViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Article No.")]
        public string ArticleNumber { get; set; } = null!;

        public string Name { get; set; } = null!;

        [Display(Name = "Design file name")]
        public string FileName { get; set; } = null!;

        public string Material { get; set; } = null!;

        [Display(Name ="Color model")]
        public string? ColorModel { get; set; }

        public Guid ClientId { get; set; }

        [Display(Name = "Client name")]
        public string ClientName { get; set; } = null!;       
    }
}
