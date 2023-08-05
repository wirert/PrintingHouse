namespace PrintingHouse.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StatusException : Exception
    {
        public StatusException()
        { }

        public StatusException(string message)
            : base(message)
        { }

        public StatusException(string message, Exception inner)
        : base(message, inner)
        { }
    }
}
