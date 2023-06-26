using System.ComponentModel.DataAnnotations;

using static PrintingHouse.Infrastructure.Constants.DataConstants.Employee;

namespace PrintingHouse.Core.Models.Account
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Employee login username 
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLenght, MinimumLength = MinUserNameLenght,
            ErrorMessage = UserNameErrorMessage)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Employee login password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Return URL, when there is redirection to login page from another page which requre login
        /// </summary>
        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// Check box value on page
        /// </summary>
        [Required]
        [Display(Name = "RememberMe")]
        public bool IsPersistent { get; set; }
    }
}
