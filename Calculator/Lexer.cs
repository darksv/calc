using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    /// <summary>
    ///     Class inteded to be a lexical analyser for the parser.
    /// </summary>
    public class Lexer : IEnumerable<Token>
    {
        /// <summary>
        ///     Mapping of symbols to coresponding TokenKind that helps tokenizing operators.
        /// </summary>
        private static readonly Dictionary<char, TokenKind> Symbols = new Dictionary<char, TokenKind>()
        {
            {',', TokenKind.Comma},
            {'+', TokenKind.Plus},
            {'-', TokenKind.Minus},
            {'*', TokenKind.Asterisk},
            {'/', TokenKind.Slash},
            {'^', TokenKind.Caret},
            {'(', TokenKind.LeftParen},
            {')', TokenKind.RightParen},
    };

        /// <summary>
        ///     Source code to tokenize.
        /// </summary>
        private readonly string _source;

        /// <summary>
        ///     Position in the source.
        /// </summary>
        private int _position;

        /// <summary>
        ///     Initializes lexer with source code.
        /// </summary>
        /// <param name="source">source string to parse</param>
        public Lexer(string source)
        {
            _source = source;
            _position = 0;
        }

        /// <summary>
        ///     Currently pointed char.
        /// </summary>
        private char Current => _source[_position];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Returns enumerator for tokens.
        /// </summary>
        /// <returns>enumerator</returns>
        public IEnumerator<Token> GetEnumerator()
        {
            while (HasNext())
                yield return GetNext();
            // Stream of tokens must be terminated with EndOfStream token 
            yield return new Token(TokenKind.EndOfSource);
        }

        /// <summary>
        ///     Moves pointer to the next char.
        /// </summary>
        private void Advance()
        {
            _position++;
        }

        /// <summary>
        ///     Skips all white spaces.
        /// </summary>
        private void SkipSpace()
        {
            while (HasNext() && char.IsWhiteSpace(Current))
                Advance();
        }

        /// <summary>
        ///     Consumes a number token.
        /// </summary>
        /// <returns>parsed value</returns>
        private double ConsumeNumber()
        {
            var value = 0.0;
            var withDot = false;
            var digitsAfterDot = 0;

            while (HasNext())
            {
                if (char.IsDigit(Current))
                {
                    value = value * 10 + (Current - '0');

                    if (withDot)
                        digitsAfterDot++;

                    Advance();
                }
                else if ((Current == '.') && !withDot)
                {
                    withDot = true;
                    Advance();
                }
                else
                {
                    break;
                }
            }

            return value / Math.Pow(10, digitsAfterDot);
        }

        /// <summary>
        ///     Consumes an identifier.
        /// </summary>
        /// <returns>consumed identifier</returns>
        private string ConsumeIdentifier()
        {
            var builder = new StringBuilder();

            while (HasNext() && char.IsLetterOrDigit(Current))
            {
                builder.Append(Current);
                Advance();
            }

            return builder.ToString();
        }

        /// <summary>
        ///     Selects current token and move to next one.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns>selected token</returns>
        private TokenKind Select(TokenKind kind)
        {
            Advance();
            return kind;
        }

        /// <summary>
        ///     Consumes other non-literal token.
        /// </summary>
        /// <returns>operator</returns>
        /// <exception cref="LexerException">when got invalid symbol</exception>
        private TokenKind ConsumeOther()
        {
            TokenKind kind;
            if (Symbols.TryGetValue(Current, out kind))
                return Select(kind);
            
            throw new LexerException($"Invalid symbol '{Current}' at position: {_position}");
        }

        /// <summary>
        ///     Checks whether there are more tokens left.
        /// </summary>
        /// <returns>true if iterator has next value otherwise false</returns>
        public bool HasNext()
        {
            return _position < _source.Length;
        }

        /// <summary>
        ///     Returns next token.
        /// </summary>
        /// <returns>next token</returns>
        public Token GetNext()
        {
            SkipSpace();

            if (!HasNext())
                return new Token(TokenKind.EndOfSource);
            else if (char.IsDigit(Current))
                return new Token(ConsumeNumber());
            else if (char.IsLetter(Current))
                return new Token(ConsumeIdentifier());
            else
                return new Token(ConsumeOther());
        }
    }
}