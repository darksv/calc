using System.Collections.Generic;
using Calculator.Parselets;

namespace Calculator
{
    /// <summary>
    ///     Class intended to parse mathematical expressions.
    /// </summary>
    public class CalculatorParser : Parser
    {
        /// <summary>
        ///     Initializes parser with specified collection of tokens.
        /// </summary>
        /// <param name="tokens">tokens to parse</param>
        public CalculatorParser(IEnumerable<Token> tokens) : base(tokens)
        {
            Register(TokenKind.Number, new NumberParselet());
            Register(TokenKind.Identifier, new IdentifierParselet());
            Register(TokenKind.Plus, new PrefixOperatorParselet());
            Register(TokenKind.Minus, new PrefixOperatorParselet());
            Register(TokenKind.LeftParen, new GroupParselet());
            Register(TokenKind.Plus, new BinaryOperatorParselet(Precedences.Addition, Associativity.Left));
            Register(TokenKind.Minus, new BinaryOperatorParselet(Precedences.Substraction, Associativity.Left));
            Register(TokenKind.Asterisk, new BinaryOperatorParselet(Precedences.Multiplication, Associativity.Left));
            Register(TokenKind.Slash, new BinaryOperatorParselet(Precedences.Division, Associativity.Left));
            Register(TokenKind.Caret, new BinaryOperatorParselet(Precedences.Exponentiation, Associativity.Right));
            Register(TokenKind.LeftParen, new FunctionCallParselet());
            Register(TokenKind.Identifier, new ImplicitMultiplicationParselet());
        }
    }
}