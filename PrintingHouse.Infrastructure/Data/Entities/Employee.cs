namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using Account;

    [Comment("Employee entity")]
    public class Employee
    {
        public Employee()
        {
            Clients = new HashSet<Client>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Employee office position id")]
        [Required]
        public int PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]        
        public virtual Position Position { get; set; } = null!;

        [Comment("Employee application user id")]
        [Required]        
        public Guid ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public virtual ICollection<Client> Clients { get; set; }

        [Comment("Soft delete property")]
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
