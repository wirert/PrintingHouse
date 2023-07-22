namespace PrintingHouse.Core.Models.Order
{
    using PrintingHouse.Infrastructure.Data.Entities.Enums;

    public class OrderViewModel
    {
        public int Id { get; set; }

        public string ArticleNo { get; set; } = null!;

        public string ArticleName { get; set; } = null!;

        public string ClientName { get; set; } = null!;

        public int Quantity { get; set; }

        public string Material { get; set; } = null!;

        public string MaterialQuantity { get; set; } = null!;

        public MeasureUnit MeasureUnit { get; set; }

        public double MaterialLength { get; set; }

        public int EmbeddedMaterialCount { get; set; }

        public string? ScrappedMaterial { get; set; }


        public string ColorModel { get; set; } = null!;

        public double Width { get; set; }

        public DateTime OrderTime { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime ExpectedPrintDate { get; set; }

        public TimeSpan ExpectedPrintTime { get; set; }

        public OrderStatus Status { get; set; }

        public string? Comment { get; set; }
    }
}
