using System;
using System.Runtime.Serialization;

namespace _20._3._19BankProject
{
    [Serializable]
    internal class CustomerNotFoundException :  ApplicationException 
    {
        public CustomerNotFoundException()
        {
        }

        public CustomerNotFoundException(string message) : base(message)
        {
        }

        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}