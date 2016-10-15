using Calculator.Expressions;

namespace Calculator.Parselets
{
    /// <summary>
    ///     Class intended to parse an identifier.
    /// </summary>
    public class IdentifierParselet : IPrefixParselet
    {
        /// <summary>
        ///     Parses an identifier.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="token"></param>
        /// <returns>parsed identifier</returns>
        public IExpression Parse(Parser parser, Token token)
        {
            return new IdentifierExpression(token.TextValue);
        }
    }
}