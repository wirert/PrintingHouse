namespace PrintingHouse.Infrastructure.Constants
{
    /// <summary>
    /// Models constants
    /// </summary>
    public static class DataConstants
    {
        /// <summary>
        /// Article model constants
        /// </summary>
        public static class Article
        {
            /// <summary>
            /// Article name maximum lenght
            /// </summary>
            public const int MaxNameLenght = 60;
            /// <summary>
            /// Article name minimum lenght
            /// </summary>
            public const int MinNameLenght = 5;
        }

        /// <summary>
        /// Client model constants
        /// </summary>
        public static class Client
        {
            /// <summary>
            /// Client name maximum lenght
            /// </summary>
            public const int MaxNameLenght = 60;
            /// <summary>
            /// Client name minimum lenght
            /// </summary>
            public const int MinNameLenght = 2;

            /// <summary>
            /// Client e-mail maximum lenght
            /// </summary>
            public const int MaxEmailLenght = 70;
            /// <summary>
            /// Client e-mail minimum lenght
            /// </summary>
            public const int MinEmailLenght = 10;
        }

        /// <summary>
        /// Order model constants
        /// </summary>
        public static class Order
        {
            /// <summary>
            /// Order comment maximum lenght
            /// </summary>
            public const int MaxCommentLenght = 600;            
        }

        /// <summary>
        /// Printing machine model constants
        /// </summary>
        public static class Machine
        {
            /// <summary>
            /// Machine name maximum lenght
            /// </summary>
            public const int MaxNameLenght = 30;
            /// <summary>
            /// Machine name minimum lenght
            /// </summary>
            public const int MinNameLenght = 2;
        }

        /// <summary>
        /// Employee model constants
        /// </summary>
        public static class Employee
        {
            /// <summary>
            /// First name maximum lenght
            /// </summary>
            public const int MaxFirstNameLenght = 20;
            /// <summary>
            /// First name minimum lenght
            /// </summary>
            public const int MinFirstNameLenght = 2;

            /// <summary>
            /// Last name maximum lenght
            /// </summary>
            public const int MaxLastNameLenght = 20;
            /// <summary>
            /// Last name minimum lenght
            /// </summary>
            public const int MinLastNameLenght = 2;

            /// <summary>
            /// Username maximum lenght
            /// </summary>
            public const int MaxUserNameLenght = 15;
            /// <summary>
            /// Username minimum lenght
            /// </summary>
            public const int MinUserNameLenght = 5;

            public const string UserNameErrorMessage = "Username must be between {2} and {1} symbols!";
            public const string FirstNameErrorMessage = "First name must be between {2} and {1} symbols!";
            public const string LastNameErrorMessage = "Last name must be between {2} and {1} symbols!";
        }
    }
}
