namespace Calculator.Expressions
{
    /// <summary>
    ///     Class intended to store identifier (variable names, function names, etc.)
    /// </summary>
    public class IdentifierExpression : IExpression
    {
        /// <summary>
        ///     Initializes an identifier expression.
        /// </summary>
        /// <param name="name">name of the identifier</param>
        public IdentifierExpression(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Name of the identifier.
        /// </summary>
        public string Name { get; }

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
            return Name;
        }
    }
}