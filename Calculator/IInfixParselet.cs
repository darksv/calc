namespace Calculator
{
    /// <summary>
    ///     Interface shared by all infix parselets.
    /// </summary>
    public interface IInfixParselet
    {
        /// <summary>
        ///     Operator's precedence.
        /// </summary>
        int Precedence { get; }

        /// <summary>
        ///     Parses an infix operator expression.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="leftExpression">left-hand expression</param>
        /// <param name="token"></param>
        /// <returns>parsed expression</returns>
        IExpression Parse(Parser parser, IExpression leftExpression, Token token);
    }
}