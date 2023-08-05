namespace PrintingHouse.Core.Exceptions
{
    public class OrderMachineException : Exception
    {
        public OrderMachineException()
        { }

        public OrderMachineException(string message)
            : base(message)
        { }

        public OrderMachineException(string message, Exception inner)
        : base(message, inner)
        { }
    }
}
