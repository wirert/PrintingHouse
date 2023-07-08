namespace PrintingHouse.Core.Models.Client
{
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants.DataConstants.Client;

    public class AddClientViewModel
    {
        [Required]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(MaxEmailLenght, MinimumLength = MinEmailLenght)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(MaxPhoneLenght)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        public int MerchantId { get; set; }
    }
}
