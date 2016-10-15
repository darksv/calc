using Calculator.Expressions;

namespace Calculator.Parselets
{
    /// <summary>
    ///     Class intended to parse a number expression.
    /// </summary>
    public class NumberParselet : IPrefixParselet
    {
        /// <summary>
        ///     Parses a number expression
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="token"></param>
        /// <returns>parsed expression</returns>
        public IExpression Parse(Parser parser, Token token)
        {
            return new NumberExpression(token.NumberValue);
        }
    }
}