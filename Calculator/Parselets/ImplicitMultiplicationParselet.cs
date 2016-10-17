using Calculator.Expressions;

namespace Calculator.Parselets
{
    /// <summary>
    ///     Class intended to parse implicit multiplication expressions, e.g. 2x, (2+x)y
    /// </summary>
    public class ImplicitMultiplicationParselet : IInfixParselet
    {
        /// <summary>
        ///     Precedence of the 'operator'.
        /// </summary>
        public int Precedence => 30;

        /// <summary>
        ///     Parses an implicit multiplication expression
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="leftExpression">expression to be multiplied by an identifier</param>
        /// <param name="token"></param>
        /// <returns>parsed expression</returns>
        public IExpression Parse(Parser parser, IExpression leftExpression, Token token)
        {
            return new BinaryExpression(TokenKind.Asterisk, leftExpression, new IdentifierExpression(token.TextValue));
        }
    }
}