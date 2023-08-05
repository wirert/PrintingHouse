namespace PrintingHouse.Core.Exceptions
{
    public class DeleteClientException : Exception
    {
        private const string Default_Message = "This client have active orders";

        public DeleteClientException()
            : base(Default_Message)
        {
        }

        public DeleteClientException(string message)
            : base(message)
        {
        }
    }
}
