using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static PrintingHouse.Infrastructure.Constants.DataConstants.Employee;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    /// <summary>
    /// Extention of identity user
    /// </summary>
    [Comment("Extention of identity user")]
    public class Employee : IdentityUser<Guid>
    {
        [Comment("Employee first name")]
        [Required]
        [MaxLength(MaxFirstNameLenght)]
        public string FirstName { get; set; } = null!;

        [Comment("Employee last name")]
        [Required]
        [MaxLength(MaxLastNameLenght)]
        public string LastName { get; set; } = null!;

        [Comment("Is active employee")]
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
