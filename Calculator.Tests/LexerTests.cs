using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System.Collections.Generic;

namespace CalculatorTests
{
    
    [TestClass]
    public class LexerTests
    {
        private static readonly Dictionary<string, Token> SingleTokenTests = new Dictionary<string, Token>
        {
            {"1", new Token(1.0)},
            {"842", new Token(842)},
            {"21.37", new Token(21.37)},
            {"o", new Token("o")},
            {"x1", new Token("x1")},
            {"abc", new Token("abc")},
            {"+", new Token(TokenKind.Plus)},
            {"-", new Token(TokenKind.Minus)},
            {"*", new Token(TokenKind.Asterisk)},
            {"/", new Token(TokenKind.Slash)},
            {"(", new Token(TokenKind.LeftParen)},
            {")", new Token(TokenKind.RightParen)},
        };
        
        private static readonly Dictionary<string, Token[]> MultipleTokensTests = new Dictionary<string, Token[]>
        {
            // Array of tokens should not contain EndOfStream token
            {"21+37",
                new Token[] {
                    new Token(21.0),
                    new Token(TokenKind.Plus),
                    new Token(37.0)
                }
            },
            {"- 1 * 3.5",
                new Token[] {
                    new Token(TokenKind.Minus),
                    new Token(1.0),
                    new Token(TokenKind.Asterisk),
                    new Token(3.5)
                }
            },
            {"(1- 3* 2) / 5",
                new Token[] {
                    new Token(TokenKind.LeftParen),
                    new Token(1.0),
                    new Token(TokenKind.Minus),
                    new Token(3.0),
                    new Token(TokenKind.Asterisk),
                    new Token(2.0),
                    new Token(TokenKind.RightParen),
                    new Token(TokenKind.Slash),
                    new Token(5.0)
                }
            },
        };
        
        [TestMethod]
        public void Tokenize_SingleToken_Tokenized()
        {
            foreach (var item in SingleTokenTests)
            {
                IEnumerable<Token> lexer = new Lexer(item.Key);
                using (var enumerator = lexer.GetEnumerator())
                {
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(item.Value, enumerator.Current);
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(new Token(TokenKind.EndOfSource), enumerator.Current);
                    Assert.IsFalse(enumerator.MoveNext());
                }
            }
        }
        
        [TestMethod]
        public void Tokenize_MultipleTokens_Tokenized()
        {
            foreach (var item in MultipleTokensTests)
            {
                IEnumerable<Token> lexer = new Lexer(item.Key);
                using (var enumerator = lexer.GetEnumerator())
                {
                    foreach (var expected in item.Value)
                    {
                        Assert.IsTrue(enumerator.MoveNext());
                        Assert.AreEqual(expected, enumerator.Current);
                    }

                    // Ensure that the last token is EndOfStream
                    Assert.IsTrue(enumerator.MoveNext());
                    Assert.AreEqual(new Token(TokenKind.EndOfSource), enumerator.Current);
                    Assert.IsFalse(enumerator.MoveNext());
                }
            }
        }
    }
}
