namespace Calculator.Parselets
{
    /// <summary>
    ///     Class intended to parse an expression with parenthesis.
    /// </summary>
    public class GroupParselet : IPrefixParselet
    {
        /// <summary>
        ///     Parses an expression with parenthesis.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="token"></param>
        /// <returns>parsed expression</returns>
        public IExpression Parse(Parser parser, Token token)
        {
            var expr = parser.Parse(0);
            parser.Consume(TokenKind.RightParen);
            return expr;
        }
    }
}