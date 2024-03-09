namespace PrintingHouse.Core.Models.Client
{
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants.DataConstants.Client;

    public class AddClientViewModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(MaxEmailLength, MinimumLength = MinEmailLength)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(MaxPhoneLength)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        public int MerchantId { get; set; }
    }
}
