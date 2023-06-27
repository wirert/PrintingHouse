using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static PrintingHouse.Infrastructure.Constants.DataConstants.Client;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Printing house client")]
    public class Client
    {
        public Client()
        {
            Articles = new HashSet<Article>();
        }

        [Comment("Client primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Client name")]
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; } = null!;

        [Comment("Client e-mail")]
        [Required]
        [MaxLength(MaxEmailLenght)]
        public string Email { get; set; } = null!;

        [Comment("Client phone number")]
        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Client's merchant id")]
        [Required]
        public Guid MerchantId { get; set; }

        [ForeignKey(nameof(MerchantId))]
        public Employee Merchant { get; set; } = null!;

        [Required]
        public ICollection<Article> Articles { get; set; }
    }
}