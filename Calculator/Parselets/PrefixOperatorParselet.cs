using Calculator.Expressions;

namespace Calculator.Parselets
{
    /// <summary>
    /// 
    /// </summary>
    public class PrefixOperatorParselet : IPrefixParselet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IExpression Parse(Parser parser, Token token)
        {
            var expr = parser.Parse(Precedence.Unary);

            return new UnaryExpression(token.Kind, expr);
        }
    }
}