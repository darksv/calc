namespace Calculator.Expressions
{
    /// <summary>
    ///     Class intended to store expression with one operand.
    /// </summary>
    public class UnaryExpression : IExpression
    {
        /// <summary>
        ///     Initializes unary expression.
        /// </summary>
        /// <param name="op">operator</param>
        /// <param name="operand">operand</param>
        public UnaryExpression(TokenKind op, IExpression operand)
        {
            Operator = op;
            Operand = operand;
        }

        /// <summary>
        ///     Operator.
        /// </summary>
        public TokenKind Operator { get; }

        /// <summary>
        ///     Argument for the operator.
        /// </summary>
        public IExpression Operand { get; }

        /// <summary>
        ///     Accepts an expression visitor.
        /// </summary>
        /// <param name="visitor">visitor to accept</param>
        public void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        ///     Converts expression to a string.
        /// </summary>
        /// <returns>string representation of the expression</returns>
        public override string ToString()
        {
            return $"({Operator} {Operand})";
        }
    }
}