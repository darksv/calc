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
        /// <exception cref="ParserException">when syntax is invalid</exception>
        /// <returns>parsed expression</returns>
        public IExpression Parse(Parser parser, IExpression leftExpression, Token token)
        {
            var callee = leftExpression as IdentifierExpression;
            if (callee == null)
            {
                // Left expression is not an identifier so this is not a function call, but implicit multiplication like 2(x+3) or (x+1)(x+3)
                var rightExpression = parser.Parse(0);
                parser.Consume(TokenKind.RightParen);
                return new BinaryOperatorExpression(TokenKind.Asterisk, leftExpression, rightExpression);
            }

            var args = new List<IExpression>();
            bool expectArgument = false;
            while (true)
            {
                if (parser.Match(TokenKind.RightParen))
                {
                    if (expectArgument)
                        throw new ParserException("Expected an argument");
                    else
                        break;
                }

                var arg = parser.Parse(0);
                args.Add(arg);

                // Expect that next token will be an argument only if there is a comma
                expectArgument = parser.Match(TokenKind.Comma);
            }
            
            return new FunctionCallExpression(callee.Name, args.ToArray());
        }

        /// <summary>
        ///     Precedence of the operator.
        /// </summary>
        public int Precedence => Precedences.FunctionCall;
    }
}