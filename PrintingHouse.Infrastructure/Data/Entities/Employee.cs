namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static Constants.DataConstants.Employee;

    /// <summary>
    /// Extention of identity user
    /// </summary>
    [Comment("Extention of identity user")]
    public class Employee : IdentityUser<Guid>
    {
        [Comment("Employee first name")]
        [MaxLength(MaxFirstNameLenght)]
        public string? FirstName { get; set; }

        [Comment("Employee last name")]
        [MaxLength(MaxLastNameLenght)]
        public string? LastName { get; set; }

        [Comment("Is active employee (soft delete property)")]        
        public bool IsActive { get; set; } = true;
    }
}
