namespace PrintingHouse.Core.Constants
{
    /// <summary>
    /// Error message constants for model validation
    /// </summary>
    public static class ModelMessageConstants
    {
        /// <summary>
        /// Application user error messages
        /// </summary>
        public static class ApplicationUser
        {
            public const string UserNameErrorMessage = "Username must be between {2} and {1} symbols!";
            public const string FirstNameErrorMessage = "First name must be between {2} and {1} symbols!";
            public const string LastNameErrorMessage = "Last name must be between {2} and {1} symbols!";
        }
    }
}
