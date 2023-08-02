namespace PrintingHouse.Core.Constants
{
    public static class ModelConstants
    {
        /// <summary>
        /// Conversion constant for article with measure unit Piece 
        /// </summary>
        public const int Article_Piece_Length = 1;

        public const string DateTimeFormat = "G";

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
