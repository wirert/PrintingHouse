namespace PrintingHouse.Core.Models.Order
{
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants.DataConstants.Order;

    public class AddOrderViewModel
    {
        public Guid ArticleId { get; set; }

        public string ArticleName { get; set; } = null!;

        public string? Material { get; set; }

        [Required]
        [Range(1, MaxQuantity)]
        public int Quantity { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(MaxCommentLenght)]
        public string? Comment { get; set; }
    }
}
