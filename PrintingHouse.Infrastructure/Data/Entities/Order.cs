﻿namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using Entities.Enums;
    using static Constants.DataConstants.Order;

    [Comment("Order from client for print")]
    public class Order
    {
        public Order()
        {
            OrderTime = DateTime.UtcNow;
        }

        [Comment("Order primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Order article id")]
        [Required]
        public Guid ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public virtual Article Article { get; set; } = null!;

        [Comment("Order article quantity")]
        [Required]
        public double Quantity { get; set; }

        [Comment("DateTime of order creation")]
        [Required]
        public DateTime OrderTime { get; set; }

        [Comment("Order deadline date if required from the client")]
        public DateTime? EndDate { get; set; }

        [Comment("Order expected print date")]
        public DateTime ExpectedPrintDate { get; set; }

        [Comment("Expected time needed for printing")]
        public TimeSpan ExpectedPrintTime { get; set; }

        [Comment("Order current status")]
        [Required]
        public OrderStatus Status { get; set; }

        [Comment("Additional information about the order.")]
        [MaxLength(MaxCommentLenght)]
        public string? Comment { get; set; }

        [Comment("Expected printing machine Id for the order")]
        public int? MachineId { get; set; }

        [ForeignKey(nameof(MachineId))]
        public virtual Machine? Machine { get; set; }

    }
}
