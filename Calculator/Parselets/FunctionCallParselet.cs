using System.Collections.Generic;
using Calculator.Expressions;

namespace Calculator.Parselets
{
    /// <summary>
    ///     Class intended to parse function call.
    /// </summary>
    public class FunctionCallParselet : IInfixParselet
    {
        /// <summary>
        ///     Parses function call expression.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="leftExpression">function to call</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IExpression Parse(Parser parser, IExpression leftExpression, Token token)
        {
            var callee = leftExpression as IdentifierExpression;
            if (callee == null)
                throw new ParseException("Only named function can be called");

            var args = new List<IExpression>();
            bool expectArgument = false;
            while (true)
            {
                if (parser.Match(TokenKind.RightParen))
                {
                    if (expectArgument)
                        throw new ParseException("Expected an argument");
                    else
                        break;
                }

                var arg = parser.Parse(0);
                args.Add(arg);

                // Expect that next token will be an argument only if there is a comma
                expectArgument = parser.Match(TokenKind.Comma);
            }
            
            return new FunctionCallExpression(callee.Name, args);
        }

        /// <summary>
        ///     Precedence of the operator.
        /// </summary>
        public int Precedence => 20;
    }
}