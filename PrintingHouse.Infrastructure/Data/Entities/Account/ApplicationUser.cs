namespace PrintingHouse.Infrastructure.Data.Entities.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static Constants.DataConstants.ApplicationUser;

    /// <summary>
    /// Extention of identity user
    /// </summary>
    [Comment("Extention of identity user")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Comment("Employee first name")]
        [MaxLength(MaxFirstNameLength)]
        public string? FirstName { get; set; }

        [Comment("Employee last name")]
        [MaxLength(MaxLastNameLength)]
        public string? LastName { get; set; }

        [Comment("Picture name of the user (nullable)")]
        [MaxLength(MaxImageNameLength)]
        public string? PictureName { get; set; }

        [Comment("Is active employee (soft delete property)")]
        public bool IsActive { get; set; }


    }
}
