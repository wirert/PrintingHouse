using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PrintingHouse.Infrastructure.Data.Entities.Enums;
using static PrintingHouse.Infrastructure.Constants.DataConstants.Order;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Order from client for print")]
    public class Order
    {
        [Comment("Order primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Order article id")]
        [Required]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public Article Article { get; set; } = null!;

        [Comment("Order article quantity")]
        [Required]
        public int Quantity { get; set; }

        [Comment("DateTime of order creation")]
        [Required]
        public DateTime OrderTime { get; set; }

        [Comment("Order expected end date if required from the client")]
        public DateTime? EndDate { get; set; }

        [Comment("Order current status")]
        [Required]
        public OrderStatus Status { get; set; }

        [Comment("Additional information about the order.")]
        [MaxLength(MaxCommentLenght)]
        public string? Comment { get; set; }
    }
}
