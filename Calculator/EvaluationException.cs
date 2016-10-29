using System;
using System.Runtime.Serialization;

namespace Calculator
{
    /// <summary>
    ///     Exception which could be thrown by expression evaluator.
    /// </summary>
    public class EvaluationException : Exception
    {
        public EvaluationException()
        {
        }
        
        public EvaluationException(string message) : base(message)
        {
        }

        public EvaluationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}