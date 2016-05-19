using System;

namespace ChainOfResponsibility
{
    public class TooManyFirstsException : Exception
    {
        public TooManyFirstsException(string Message) : base(Message)
        {
            
        }
    }
}