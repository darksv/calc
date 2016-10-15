using System.Globalization;

namespace Calculator.Expressions
{
    /// <summary>
    ///     Class intended to store number literals.
    /// </summary>
    public class NumberExpression : IExpression
    {
        /// <summary>
        ///     Initializes expression.
        /// </summary>
        /// <param name="number">number to store</param>
        public NumberExpression(double number)
        {
            Number = number;
        }

        /// <summary>
        ///     Number stored in expression.
        /// </summary>
        public double Number { get; }

        /// <summary>
        ///     Accepts an expression visitor.
        /// </summary>
        /// <param name="visitor">visitor to accept</param>
        public void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        ///     Converts expression to string.
        /// </summary>
        /// <returns>string representation of the expression</returns>
        public override string ToString()
        {
            return Number.ToString(CultureInfo.CurrentCulture);
        }
    }
}