using System;

namespace Calculator
{
    /// <summary>
    ///     Exception thrown by parser.
    /// </summary>
    public class ParserException : Exception
    {
        /// <summary>
        ///     Initializes an parser exception.
        /// </summary>
        public ParserException()
        {
        }

        /// <summary>
        ///     Initializes an parser exception with specified message.
        /// </summary>
        /// <param name="message">message of the exception</param>
        public ParserException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes an parser exception with specified message being caused by another exception.
        /// </summary>
        /// <param name="message">message of the exception</param>
        /// <param name="inner">exception which caused this one</param>
        public ParserException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}