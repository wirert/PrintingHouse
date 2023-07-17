namespace PrintingHouse.Core.Models.Article
{
    using System.ComponentModel.DataAnnotations;

    public class AllArticleViewModel
    {
        public Guid Id { get; set; }
       
        public string Name { get; set; } = null!;

        [Display(Name = "Design file name")]
        public string ImageName { get; set; } = null!;

        public string Material { get; set; } = null!;

        [Display(Name ="Color model")]
        public string? ColorModel { get; set; }

        public int ClientId { get; set; }

        [Display(Name = "Client name")]
        public string ClientName { get; set; } = null!;
    }
}
