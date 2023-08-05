namespace PrintingHouse.Core.Exceptions
{
    public class EmployeeSelfChangeException : Exception
    {        
        public EmployeeSelfChangeException()
        { }

        public EmployeeSelfChangeException(string message)
            : base(message)
        { }

        public EmployeeSelfChangeException(string message, Exception inner)
        : base(message, inner)
        { }
    }
}
