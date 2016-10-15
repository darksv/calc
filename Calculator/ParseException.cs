using System;

namespace Calculator
{
    /// <summary>
    ///     Exception thrown by parser.
    /// </summary>
    public class ParseException : Exception
    {
        /// <summary>
        ///     Initializes an parser exception.
        /// </summary>
        public ParseException()
        {
        }

        /// <summary>
        ///     Initializes an parser exception with specified message.
        /// </summary>
        /// <param name="message">message of the exception</param>
        public ParseException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes an parser exception with specified message being caused by another exception.
        /// </summary>
        /// <param name="message">message of the exception</param>
        /// <param name="inner">exception which caused this one</param>
        public ParseException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}