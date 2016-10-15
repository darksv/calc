using System.Collections.Generic;
using System.Globalization;

namespace Calculator
{
    /// <summary>
    ///     Enum for token kinds.
    /// </summary>
    public enum TokenKind
    {
        Number,
        Identifier,
        Comma,
        Plus,
        Minus,
        Asterisk,
        Slash,
        LeftParen,
        RightParen,
        End
    }
    
    /// <summary>
    ///     Class intended to be an basic lexical unit for the parser.
    /// </summary>
    public class Token
    {
        /// <summary>
        ///     Initializes token with specified kind.
        /// </summary>
        /// <param name="kind">kind of the token</param>
        public Token(TokenKind kind)
        {
            Kind = kind;
        }

        /// <summary>
        ///     Initializes token with a number.
        /// </summary>
        /// <param name="numberValue">number to store</param>
        public Token(double numberValue)
        {
            Kind = TokenKind.Number;
            NumberValue = numberValue;
        }

        /// <summary>
        ///     Initializes token with a string.
        /// </summary>
        /// <param name="textValue">text to store</param>
        public Token(string textValue)
        {
            Kind = TokenKind.Identifier;
            TextValue = textValue;
        }

        /// <summary>
        ///     Kind of the token.
        /// </summary>
        public TokenKind Kind { get; }

        /// <summary>
        ///     Number value of the token.
        /// </summary>
        public double NumberValue { get; }

        /// <summary>
        ///     Text value of the token.
        /// </summary>
        public string TextValue { get; }

        /// <summary>
        ///     Compares two objects.
        /// </summary>
        /// <param name="obj">other object</param>
        /// <returns>true if objects are equal, otherwise, false</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Token;

            return (other != null)
                   && Kind.Equals(other.Kind)
                   && NumberValue.Equals(other.NumberValue)
                   && TextValue.Equals(other.TextValue);
        }

        /// <summary>
        ///     Calculates hash code value of the object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Kind.GetHashCode() ^ NumberValue.GetHashCode() ^ TextValue.GetHashCode();
        }

        /// <summary>
        ///     Converts the token to a string.
        /// </summary>
        /// <returns>string representation of the token</returns>
        public override string ToString()
        {
            switch (Kind)
            {
                case TokenKind.Identifier:
                    return TextValue;
                case TokenKind.Number:
                    return NumberValue.ToString(CultureInfo.InvariantCulture);
                default:
                    return Kind.ToString();
            }
        }
    }
}