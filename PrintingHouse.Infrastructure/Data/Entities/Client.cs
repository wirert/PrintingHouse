namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using static Constants.DataConstants.Client;

    [Comment("Printing house client")]
    public class Client
    {
        public Client()
        {
            Id = Guid.NewGuid();
            Articles = new HashSet<Article>();
            IsActive = true;
        }

        [Comment("Client primary key")]
        [Key]
        public Guid Id { get; set; }

        public int ClientNumber { get; set; }

        [Comment("Client name")]
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; } = null!;

        [Comment("Client e-mail")]
        [Required]
        [MaxLength(MaxEmailLength)]
        public string Email { get; set; } = null!;

        [Comment("Client phone number")]
        [Required]
        [MaxLength(MaxPhoneLength)]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Client's merchant id")]
        [Required]
        public int MerchantId { get; set; }

        [ForeignKey(nameof(MerchantId))]
        public virtual Employee Merchant { get; set; } = null!;

        public virtual ICollection<Article> Articles { get; set; }

        [Comment("Soft delete propery")]
        public bool IsActive { get; set; }
    }
}