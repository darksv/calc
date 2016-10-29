using Calculator.Expressions;

namespace Calculator.Parselets
{
    /// <summary>
    ///     Class that is used to parse expressions with prefix operator e.g. unary minus, unary plus
    /// </summary>
    public class PrefixOperatorParselet : IPrefixParselet
    {
        /// <summary>
        ///     Parses an prefix operator expression.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="token"></param>
        /// <returns>parsed expression</returns>
        public IExpression Parse(Parser parser, Token token)
        {
            var expr = parser.Parse(Precedences.Unary);

            return new UnaryOperatorExpression(token.Kind, expr);
        }
    }
}