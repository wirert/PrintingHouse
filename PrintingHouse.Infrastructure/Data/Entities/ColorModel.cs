﻿namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    using static Infrastructure.Constants.DataConstants.ColorModel;

    [Comment("Printing color model")]
    public class ColorModel
    {
        public ColorModel()
        {
            ArticleColors = new HashSet<ArticleColor>();
            MaterialsColorModel = new HashSet<MaterialColorModel>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Color model name")]
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; } = null!;

        public ICollection<MaterialColorModel> MaterialsColorModel { get; set; }

        public virtual ICollection<ArticleColor> ArticleColors { get; set; }
    }
}
