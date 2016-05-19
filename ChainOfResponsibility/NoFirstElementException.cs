using System;

namespace ChainOfResponsibility
{
    public class NoFirstElementException : Exception
    {
        public NoFirstElementException(string Message) : base(Message)
        {
            
        }
    }
}
