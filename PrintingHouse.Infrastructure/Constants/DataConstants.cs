namespace PrintingHouse.Infrastructure.Constants
{
    public static class DataConstants
    {
        public static class Article
        {
            public const int MaxNameLenght = 60;
            public const int MinNameLenght = 5;
        }

        public static class Client
        {
            public const int MaxNameLenght = 60;
            public const int MinNameLenght = 2;

            public const int MaxEmailLenght = 70;
            public const int MinEmailLenght = 10;
        }

        public static class Order
        {
            public const int MaxCommentLenght = 600;            
        }

        public static class Machine
        {
            public const int MaxNameLenght = 30;
            public const int MinNameLenght = 2;
        }
    }
}
