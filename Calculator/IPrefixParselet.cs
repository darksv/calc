namespace Calculator
{
    /// <summary>
    ///     Interface shared by all prefix parselets.
    /// </summary>
    public interface IPrefixParselet
    {
        /// <summary>
        ///     Parse an prefix operator expression.
        /// </summary>
        /// <param name="parser">parser object</param>
        /// <param name="token">token representing an operator</param>
        /// <returns>parsed expression</returns>
        IExpression Parse(Parser parser, Token token);
    }
}