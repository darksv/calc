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

            var b = _numbers.Pop();
            var a = _numbers.Pop();

            switch (expression.Operator)
            {
                case TokenKind.Plus:
                    _numbers.Push(a + b);
                    break;
                case TokenKind.Minus:
                    _numbers.Push(a - b);
                    break;
                case TokenKind.Asterisk:
                    _numbers.Push(a * b);
                    break;
                case TokenKind.Slash:
                    _numbers.Push(a / b);
                    break;
                default:
                    throw new Exception($"Invalid operator {expression.Operator}");
            }
        }

        /// <summary>
        ///     Calculates result of the unary operator expression and pushes it onto the stack.
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        public void Visit(UnaryExpression expression)
        {
            expression.Operand.Accept(this);

            switch (expression.Operator)
            {
                case TokenKind.Plus:
                    // Plus had no effect on a number
                    break;
                case TokenKind.Minus:
                    _numbers.Push(-_numbers.Pop());
                    break;
                default:
                    throw new ParseException($"Unsupported operator {expression.Operator}");
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