using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Expressions;

namespace Calculator
{
    /// <summary>
    ///     Class intended to evaluate an expression and return the result.
    /// </summary>
    public class EvaluationVisitor : IExpressionVisitor
    {
        private readonly Stack<double> _numbers = new Stack<double>();

        /// <summary>
        ///     Functions that handle prefix unary operations.
        /// </summary>
        private static readonly Dictionary<TokenKind, Func<double, double>> PrefixOperators =
            new Dictionary<TokenKind, Func<double, double>>()
            {
                {TokenKind.Plus, (o) => +o},
                {TokenKind.Minus, (o) => -o},
            };

        /// <summary>
        ///     Functions that handle infix binary operations.
        /// </summary>
        private static readonly Dictionary<TokenKind, Func<double, double, double>> BinaryOperators = 
            new Dictionary<TokenKind, Func<double, double, double>>()
            {
                {TokenKind.Plus, (l, r) => l + r},
                {TokenKind.Minus, (l, r) => l - r},
                {TokenKind.Asterisk, (l, r) => l * r},
                {TokenKind.Slash, (l, r) => l / r},
            };

        /// <summary>
        ///     Pushes value of the expression onto the stack.
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        public void Visit(NumberExpression expression)
        {
            _numbers.Push(expression.Number);
        }

        /// <summary>
        ///     Pushes value being under the identifier onto the stack.
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        public void Visit(IdentifierExpression expression)
        {
            _numbers.Push(2137);
        }

        /// <summary>
        ///     Calculates value of the binary operator expression and pushes it onto the stack.
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        public void Visit(BinaryExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);

            Func<double, double, double> func;
            if (BinaryOperators.TryGetValue(expression.Operator, out func))
            {
                // Arguments should be popped in reversed order
                var b = _numbers.Pop();
                var a = _numbers.Pop();

                var result = func(a, b);

                _numbers.Push(result);
            }
            else
            {
                throw new Exception($"Unsupported binary operator {expression.Operator}");
            }
        }

        /// <summary>
        ///     Calculates result of the unary operator expression and pushes it onto the stack.
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        public void Visit(UnaryExpression expression)
        {
            expression.Operand.Accept(this);

            Func<double, double> func;
            if (PrefixOperators.TryGetValue(expression.Operator, out func))
            {
                var arg = _numbers.Pop();

                var result = func(arg);

                _numbers.Push(result);
            }
            else
            {
                throw new Exception($"Unsupported prefix unary operator {expression.Operator}");
            }
        }

        /// <summary>
        ///     Evaluates result of the function call and pushes result onto the stack.
        /// </summary>
        /// <param name="expression">expression to evaluate</param>
        public void Visit(FunctionCallExpression expression)
        {
            var evaluatedArgs = new List<double>();

            foreach (var arg in expression.Arguments.AsEnumerable().Reverse())
            {
                arg.Accept(this);
                evaluatedArgs.Add(_numbers.Pop());
            }

            _numbers.Push(evaluatedArgs.Sum());
        }

        /// <summary>
        ///     Returns value from top of the stack.
        /// </summary>
        /// <returns>value from top of the stack</returns>
        public double Value => _numbers.Peek();
    }
}