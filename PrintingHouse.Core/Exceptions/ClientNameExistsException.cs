namespace PrintingHouse.Core.Exceptions
{
    public class ClientNameExistsException : Exception
    {
        private const string MESSAGE = "There is already a client with that name!";
        public ClientNameExistsException()
            :base(MESSAGE)
        {
        }

        public ClientNameExistsException(string message)
            : base(message)
        { }
    }
}
