using System;

namespace Sat.Recruitment.Api.Core.Exceptions
{
    [Serializable]
    public class DuplucateUserException : Exception
    {
        public DuplucateUserException() { }

        public DuplucateUserException(string message)
            : base(message) { }

        public DuplucateUserException(string message, Exception inner)
            : base(message, inner) { }
    }
}
