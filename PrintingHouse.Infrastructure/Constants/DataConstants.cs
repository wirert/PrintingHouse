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
            /// Article name maximum length
            /// </summary>
            public const int MaxNameLength = 60;
            /// <summary>
            /// Article name minimum length
            /// </summary>
            public const int MinNameLength = 5;
            /// <summary>
            /// Article design image name maximum length
            /// </summary>
            public const int MaxImageNameLength = 100;
            /// <summary>
            /// Article design image name minimum length
            /// </summary>
            public const int MinImageNameLength = 1;

            /// <summary>
            /// Maximum lenght of Article number string
            /// </summary>
            public const int MaxArticleNoLength = 20;

            public const double MaxLength = 1000;
        }

        /// <summary>
        /// Client model constants
        /// </summary>
        public static class Client
        {
            /// <summary>
            /// Client name maximum length
            /// </summary>
            public const int MaxNameLength = 60;
            /// <summary>
            /// Client name minimum length
            /// </summary>
            public const int MinNameLength = 2;

            /// <summary>
            /// Client e-mail maximum length
            /// </summary>
            public const int MaxEmailLength = 70;
            /// <summary>
            /// Client e-mail minimum length
            /// </summary>
            public const int MinEmailLength = 10;

            /// <summary>
            /// Client phone number maximum length
            /// </summary>
            public const int MaxPhoneLength = 20;
        }

        /// <summary>
        /// Order model constants
        /// </summary>
        public static class Order
        {
            /// <summary>
            /// Order comment maximum length
            /// </summary>
            public const int MaxCommentLength = 600;
            /// <summary>
            /// Order maximum quantity
            /// </summary>
            public const int MaxQuantity = 500000;
        }

        /// <summary>
        /// Printing machine model constants
        /// </summary>
        public static class Machine
        {
            /// <summary>
            /// Machine name maximum length
            /// </summary>
            public const int MaxNameLength = 30;
            /// <summary>
            /// Machine name minimum length
            /// </summary>
            public const int MinNameLength = 2;

            /// <summary>
            /// Machine name maximum length
            /// </summary>
            public const int MaxModelLength = 40;
            /// <summary>
            /// Machine name minimum length
            /// </summary>
            public const int MinModelLength = 2;
        }

        /// <summary>
        /// ColorModel model constants
        /// </summary>
        public static class ColorModel
        {
            /// <summary>
            /// ColorModel name maximum length
            /// </summary>
            public const int MaxNameLength = 10;
            /// <summary>
            /// ColorModel minimum length
            /// </summary>
            public const int MinNameLength = 1;
        }

        /// <summary>
        /// Consumable model constants
        /// </summary>
        public static class Consumable
        {
            /// <summary>
            /// Consumable type name maximum length
            /// </summary>
            public const int MaxTypeLength = 10;
            /// <summary>
            /// Consumable type minimum length
            /// </summary>
            public const int MinTypeLength = 1;
        }


        /// <summary>
        /// Consumable model constants
        /// </summary>
        public static class Material
        {
            /// <summary>
            /// Material type name maximum length
            /// </summary>
            public const int MaxTypeLength = 15;
            /// <summary>
            /// Material type minimum length
            /// </summary>
            public const int MinTypeLength = 2;
        }

        /// <summary>
        /// Application user model constants
        /// </summary>
        public static class ApplicationUser
        {
            /// <summary>
            /// First name maximum length
            /// </summary>
            public const int MaxFirstNameLength = 20;
            /// <summary>
            /// First name minimum length
            /// </summary>
            public const int MinFirstNameLength = 2;

            /// <summary>
            /// Last name maximum length
            /// </summary>
            public const int MaxLastNameLength = 20;
            /// <summary>
            /// Last name minimum length
            /// </summary>
            public const int MinLastNameLength = 2;

            /// <summary>
            /// Username maximum length
            /// </summary>
            public const int MaxUserNameLength = 15;
            /// <summary>
            /// Username minimum length
            /// </summary>
            public const int MinUserNameLength = 5;

            /// <summary>
            /// Article design image name maximum length
            /// </summary>
            public const int MaxImageNameLength = 60;
            /// <summary>
            /// Article design image name minimum length
            /// </summary>
            public const int MinImageNameLength = 1;
        }

        /// <summary>
        /// Office position model constants
        /// </summary>
        public static class Position
        {
            /// <summary>
            /// Office position maximum length
            /// </summary>
            public const int MaxPositionLength = 20;
            /// <summary>
            /// Office position minimum length
            /// </summary>
            public const int MinPositionLength = 3;
        }
    }
}
