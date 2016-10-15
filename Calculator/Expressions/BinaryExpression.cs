namespace Calculator.Expressions
{
    /// <summary>
    ///     Class intended to store expression with two operands.
    /// </summary>
    public class BinaryExpression : IExpression
    {
        /// <summary>
        ///     Initializes a binary expression.
        /// </summary>
        /// <param name="op">operator</param>
        /// <param name="left">left operand</param>
        /// <param name="right">right operand</param>
        public BinaryExpression(TokenKind op, IExpression left, IExpression right)
        {
            Operator = op;
            Left = left;
            Right = right;
        }

        /// <summary>
        ///     Left-hand operand.
        /// </summary>
        public IExpression Left { get; }

        /// <summary>
        ///     Operator.
        /// </summary>
        public TokenKind Operator { get; }

        /// <summary>
        ///     Right-hand operand.
        /// </summary>
        public IExpression Right { get; }

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
            return $"({Left}{Operator}{Right})";
        }
    }
}