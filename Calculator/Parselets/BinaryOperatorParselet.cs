using Calculator.Expressions;

namespace Calculator.Parselets
{
    /// <summary>
    ///     Class intended to be a binary operator expression parselet.
    /// </summary>
    public class BinaryOperatorParselet : IInfixParselet
    {
        /// <summary>
        ///     Initializes parselet with operator's precedence and associavity.
        /// </summary>
        /// <param name="precedence">precedence of the operator</param>
        /// <param name="associativity">associavity of the operator</param>
        public BinaryOperatorParselet(int precedence, Associativity associativity)
        {
            Precedence = precedence;
            Associativity = associativity;
        }

        /// <summary>
        ///     Operator's associavity.
        /// </summary>
        public Associativity Associativity { get; }

        /// <summary>
        ///     Parses a binary operator expression.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="leftExpression">expression on left side of the operator</param>
        /// <param name="token"></param>
        /// <returns>parsed expression</returns>
        public IExpression Parse(Parser parser, IExpression leftExpression, Token token)
        {
            // We need to lower the precedence to properly handle the operator's right-associativity.
            // By default it would be left-associative.
            var rightExpression = parser.Parse(Precedence - (Associativity == Associativity.Right ? 1 : 0));
            return new BinaryOperatorExpression(token.Kind, leftExpression, rightExpression);
        }

        /// <summary>
        ///     Operator's precedence.
        /// </summary>
        public int Precedence { get; }
    }
}