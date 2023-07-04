namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using static Constants.DataConstants.Position;

    [Comment("Office position")]
    public class Position
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Position name")]
        [Required]
        [MaxLength(MaxPositionLenght)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        [Comment("Soft delete property")]
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
