using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    ///     Exception that can be thrown by lexer.
    /// </summary>
    public class LexerException : Exception
    {
        public LexerException()
        {
        }

        public LexerException(string message) : base(message)
        {
        }

        public LexerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LexerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
