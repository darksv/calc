using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    ///      Class intended to parse tokens into expression.
    /// </summary>
    public class Parser
    {
        private readonly IEnumerator<Token> _enumerator;

        private readonly Dictionary<TokenKind, IInfixParselet> _infixParselets =
            new Dictionary<TokenKind, IInfixParselet>();

        private readonly Dictionary<TokenKind, IPrefixParselet> _prefixParselets =
            new Dictionary<TokenKind, IPrefixParselet>();

        private readonly List<Token> _queuedTokens = new List<Token>();

        private IEnumerable<Token> _tokens;

        /// <summary>
        ///     Initializes a parser for collection of tokens.
        /// </summary>
        /// <param name="tokens">tokens to parse</param>
        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens;
            _enumerator = tokens.GetEnumerator();
        }

        /// <summary>
        ///     Registers a new prefix parselet.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="parselet">prefix parselet to register</param>
        public void Register(TokenKind kind, IPrefixParselet parselet)
        {
            _prefixParselets.Add(kind, parselet);
        }

        /// <summary>
        ///     Registers a new infix parselet.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="parselet">infix parselet to register</param>
        public void Register(TokenKind kind, IInfixParselet parselet)
        {
            _infixParselets.Add(kind, parselet);
        }

        /// <summary>
        ///     Returns prefix parselet for specified token kind.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns>parselet or null if parselet does not exist</returns>
        private IPrefixParselet GetPrefixParseletIfExists(TokenKind kind)
        {
            IPrefixParselet parselet;
            _prefixParselets.TryGetValue(kind, out parselet);
            return parselet;
        }

        /// <summary>
        ///     Return infix parselet for specified token kind.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns>parselet or null if parselet does not exist</returns>
        private IInfixParselet GetInfixParseletIfExists(TokenKind kind)
        {
            IInfixParselet parselet;
            _infixParselets.TryGetValue(kind, out parselet);
            return parselet;
        }

        /// <summary>
        ///     Parses expression with given precedence.
        /// </summary>
        /// <param name="precedence">precedence</param>
        /// <returns>parsed expression</returns>
        public IExpression Parse(int precedence)
        {
            var token = Consume();

            var prefix = GetPrefixParseletIfExists(token.Kind);
            if (prefix == null)
                throw new ParseException($"Unexpected token {token}");

            var left = prefix.Parse(this, token);

            // We are parsing other expressions with lower precedence
            // leaving our to be processed later
            while (precedence < GetPrecedence())
            {
                token = Consume();

                var parselet = GetInfixParseletIfExists(token.Kind);
                if (parselet != null)
                {
                    left = parselet.Parse(this, left, token);
                }
                else
                {
                    throw new ParseException("watwat");
                }
            }

            return left;
        }

        /// <summary>
        ///     Parses a whole expression
        /// </summary>
        /// <returns>parsed expression</returns>
        public IExpression Parse()
        {
            var expr = Parse(0);

//            if (LookAhead(0).Kind != TokenKind.End)
//                throw new ParseException($"Unexpected token {LookAhead(0).Kind}");

            return expr;
        }

        /// <summary>
        ///     Consumes token with given kind.
        /// </summary>
        /// <param name="expected">expected token kind</param>
        /// <returns>consumed token</returns>
        public Token Consume(TokenKind expected)
        {
            var token = LookAhead(0);
            if (token.Kind != expected)
                throw new ParseException($"Unexpected token {token}");

            return Consume();
        }

        /// <summary>
        ///     Pops one token from queue.
        /// </summary>
        /// <returns>popped token</returns>
        public Token Consume()
        {
            var token = LookAhead(0);
            if (token.Kind == TokenKind.End)
                throw new ParseException("Unexpected end of tokens");
            
            _queuedTokens.RemoveAt(0);
            return token;
        }

        /// <summary>
        ///     Checks whether current token has specified kind. If it matches, consumes it, otherwise not.
        /// </summary>
        /// <param name="expected"></param>
        /// <returns>true if tokens match, otherwise, false</returns>
        public bool Match(TokenKind expected)
        {
            var token = LookAhead(0);
            if (token.Kind != expected)
                return false;

            Consume();
            return true;
        }

        /// <summary>
        ///     Preloads tokens to queue.
        /// </summary>
        /// <param name="distance">index of token on queue to return</param>
        /// <returns>token</returns>
        public Token LookAhead(int distance)
        {
            // Load new tokens into a queue until we can get the one specified by distance
            while (distance >= _queuedTokens.Count)
            {
                if (_enumerator.MoveNext())
                    _queuedTokens.Add(_enumerator.Current);
                else
                    return new Token(TokenKind.End);
            }

            return _queuedTokens[distance];
        }

        /// <summary>
        ///     Return precedence of binary operator which is actually parsed.
        /// </summary>
        /// <returns>precedence of an operator</returns>
        private int GetPrecedence()
        {
            var token = LookAhead(0);
            return GetInfixParseletIfExists(token.Kind)?.Precedence ?? 0;
        }
    }
}