namespace PrintingHouse.Core.Exceptions
{
    public class StatusPermitionException : Exception
    {
        public StatusPermitionException() 
        { }

        public StatusPermitionException(string message)
            : base(message) 
        { }

        public StatusPermitionException(string message, Exception inner)
        : base(message, inner)
        { }
    }
}
