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

        public double? MaterialQuantity { get; set; }

        public int Width { get; set; }

        public DateTime OrderTime { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime ExpectedPrintDate { get; set; }

        public TimeSpan ExpectedPrintTime { get; set; }

        public OrderStatus Status { get; set; }

        public string? Comment { get; set; }

        public int? MachineId { get; set; }
    }
}
