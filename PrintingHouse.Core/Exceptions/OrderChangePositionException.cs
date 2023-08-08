namespace PrintingHouse.Core.Exceptions
{
    using System;

    public class OrderChangePositionException : Exception
    {
        public OrderChangePositionException()
        { }

        public OrderChangePositionException(string message)
            : base(message)
        { }
    }
}
