using System.Collections.Generic;

namespace Calculator.Expressions
{
    /// <summary>
    ///     Class to store function call expression.
    /// </summary>
    public class FunctionCallExpression : IExpression
    {
        /// <summary>
        ///     Initializes function call expression.
        /// </summary>
        /// <param name="callee">function to call</param>
        /// <param name="args">arguments for the function</param>
        public FunctionCallExpression(IExpression callee, List<IExpression> args)
        {
            Callee = callee;
            Arguments = args;
        }

        /// <summary>
        ///     Function to call.
        /// </summary>
        public IExpression Callee { get; }

        /// <summary>
        ///     Arguments passed to the function.
        /// </summary>
        public List<IExpression> Arguments { get; }

        /// <summary>
        ///     Accepts an expression visitor.
        /// </summary>
        /// <param name="visitor">visitor to accept</param>
        public void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        ///     Converts the expression to a string.
        /// </summary>
        /// <returns>string representation of the expression</returns>
        public override string ToString()
        {
            return $"{Callee}({Arguments})";
        }
    }
}