namespace Calculator
{
    /// <summary>
    ///     Interface shared by all expressions.
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        ///     Accepts a visitor.
        /// </summary>
        /// <param name="visitor">visitor to apply</param>
        void Accept(IExpressionVisitor visitor);
    }
}