﻿using System.Collections.Generic;
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
            Register(TokenKind.Plus, new BinaryOperatorParselet(Precedence.Addition, Association.Left));
            Register(TokenKind.Minus, new BinaryOperatorParselet(Precedence.Substraction, Association.Left));
            Register(TokenKind.Asterisk, new BinaryOperatorParselet(Precedence.Multiplication, Association.Left));
            Register(TokenKind.Slash, new BinaryOperatorParselet(Precedence.Division, Association.Left));
            Register(TokenKind.LeftParen, new FunctionCallParselet());
        }
    }
}