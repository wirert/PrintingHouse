namespace PrintingHouse.Core.Models.Article
{
    public class AllArticleViewModel
    {
        public Guid Id { get; set; }
       
        public string Name { get; set; } = null!;
       
        public string ImageName { get; set; } = null!;

        public string Material { get; set; } = null!;

        public string? ColorModel { get; set; }

        public int ClientId { get; set; }
       
        public string ClientName { get; set; } = null!;
    }
}
