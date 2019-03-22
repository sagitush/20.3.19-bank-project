using System;
using System.Runtime.Serialization;

namespace _20._3._19BankProject
{
    [Serializable]
    internal class AccountNotFoundException : ApplicationException
    {
        public AccountNotFoundException()
        {
        }

        public AccountNotFoundException(string message) : base(message)
        {
        }

        public AccountNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}