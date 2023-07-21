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
            /// <summary>
            /// Article design image name maximum lenght
            /// </summary>
            public const int MaxImageNameLenght = 60;
            /// <summary>
            /// Article design image name minimum lenght
            /// </summary>
            public const int MinImageNameLenght = 1;

            /// <summary>
            /// Maximum lenght of Article number string
            /// </summary>
            public const int MaxArticleNoLenght = 20;

            public const double MaxLength = 1000;
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

            /// <summary>
            /// Client phone number maximum lenght
            /// </summary>
            public const int MaxPhoneLenght = 20;
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

            public const int MaxQuantity = 500000;
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

            /// <summary>
            /// Machine name maximum lenght
            /// </summary>
            public const int MaxModelLenght = 40;
            /// <summary>
            /// Machine name minimum lenght
            /// </summary>
            public const int MinModelLenght = 2;
        }

        /// <summary>
        /// ColorModel model constants
        /// </summary>
        public static class ColorModel
        {
            /// <summary>
            /// ColorModel name maximum lenght
            /// </summary>
            public const int MaxNameLenght = 10;
            /// <summary>
            /// ColorModel minimum lenght
            /// </summary>
            public const int MinNameLenght = 1;
        }

        /// <summary>
        /// Consumable model constants
        /// </summary>
        public static class Consumable
        {
            /// <summary>
            /// Consumable type name maximum lenght
            /// </summary>
            public const int MaxTypeLenght = 10;
            /// <summary>
            /// Consumable type minimum lenght
            /// </summary>
            public const int MinTypeLenght = 1;
        }


        /// <summary>
        /// Consumable model constants
        /// </summary>
        public static class Material
        {
            /// <summary>
            /// Material type name maximum lenght
            /// </summary>
            public const int MaxTypeLenght = 15;
            /// <summary>
            /// Material type minimum lenght
            /// </summary>
            public const int MinTypeLenght = 2;
        }

        /// <summary>
        /// Application user model constants
        /// </summary>
        public static class ApplicationUser
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

            /// <summary>
            /// Article design image name maximum lenght
            /// </summary>
            public const int MaxImageNameLenght = 60;
            /// <summary>
            /// Article design image name minimum lenght
            /// </summary>
            public const int MinImageNameLenght = 1;
        }

        /// <summary>
        /// Office position model constants
        /// </summary>
        public static class Position
        {
            /// <summary>
            /// Office position maximum lenght
            /// </summary>
            public const int MaxPositionLenght = 20;
            /// <summary>
            /// Office position minimum lenght
            /// </summary>
            public const int MinPositionLenght = 3;
        }
    }
}
