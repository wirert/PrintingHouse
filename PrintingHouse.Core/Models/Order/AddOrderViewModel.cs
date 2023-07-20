namespace PrintingHouse.Core.Models.Order
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    using static Infrastructure.Constants.DataConstants.Order;

    public class AddOrderViewModel
    {
        public Guid ArticleId { get; set; }

        public string ArticleName { get; set; } = null!;

        [Required]
        [Range(1, MaxQuantity)]
        public double Quantity { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(MaxCommentLenght)]
        public string? Comment { get; set; }
    }
}
