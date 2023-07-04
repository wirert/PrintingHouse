namespace PrintingHouse.Core.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using static Constants.ModelMessageConstants.ApplicationUser;
    using static Infrastructure.Constants.DataConstants.ApplicationUser;

    /// <summary>
    /// Register new employee view model
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Employee Username for login
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLenght, MinimumLength = MinUserNameLenght,
            ErrorMessage = UserNameErrorMessage)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Employee Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Employee password
        /// </summary>
        [Required]
        [Compare(nameof(PasswordRepeat))]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Employee confurm password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string PasswordRepeat { get; set; } = null!;

        /// <summary>
        /// Employee first name
        /// </summary>
        [Required]
        [StringLength(MaxFirstNameLenght, MinimumLength = MinFirstNameLenght, 
            ErrorMessage = FirstNameErrorMessage)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Employee last name
        /// </summary>
        [Required]
        [StringLength(MaxLastNameLenght, MinimumLength = MinLastNameLenght, 
            ErrorMessage = LastNameErrorMessage)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Employee phone number
        /// </summary>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
