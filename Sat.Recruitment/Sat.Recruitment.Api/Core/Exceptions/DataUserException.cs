using System;

namespace Sat.Recruitment.Api.Core.Exceptions
{
    [Serializable]
    public class DataUserException : Exception
    {
        public DataUserException() { }

        public DataUserException(string message)
            : base(message) { }

        public DataUserException(string message, Exception inner)
            : base(message, inner) { }
    }
}
