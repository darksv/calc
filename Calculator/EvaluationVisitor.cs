using System;
using System.Collections.Generic;
using Calculator.Expressions;

namespace Calculator
{
    /// <summary>
    ///     Class intended to evaluate an expression and return the result.
    /// </summary>
    public class EvaluationVisitor : IExpressionVisitor
    {
        /// <summary>
        ///     Stack with numbers.
        /// </summary>
        private readonly Stack<double> _numbers = new Stack<double>();

        /// <summary>
        ///     Evaluation context with functions and constants.
        /// </summary>
        private readonly EvaluationContext _context;
        
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
                {TokenKind.Caret, Math.Pow}
            };

        /// <summary>
        ///     Initializes class with the given context.
        /// </summary>
        /// <param name="context"></param>
        public EvaluationVisitor(EvaluationContext context)
        {
            _context = context;
        }

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
        /// <exception cref="EvaluationException">when constant is not declared</exception>
        public void Visit(IdentifierExpression expression)
        {
            var result = _context.TryGetConstant(expression.Name);
            if (result.HasValue)
                _numbers.Push(result.Value);
            else
                throw new EvaluationException($"Undeclared variable {expression.Name}");
        }

        /// <summary>
        ///     Calculates value of the binary operator expression and pushes it onto the stack.
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        /// <exception cref="EvaluationException">when binary operator is not supported</exception>
        public void Visit(BinaryOperatorExpression expression)
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
                throw new EvaluationException($"Unsupported binary operator {expression.Operator}");
            }
        }

        /// <summary>
        ///     Calculates result of the unary operator expression and pushes it onto the stack.
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        /// <exception cref="EvaluationException">when prefix unary operator is not supported</exception>
        public void Visit(UnaryOperatorExpression expression)
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
                throw new EvaluationException($"Unsupported prefix unary operator {expression.Operator}");
            }
        }

        /// <summary>
        ///     Evaluates result of the function call and pushes result onto the stack.
        /// </summary>
        /// <param name="expression">expression to evaluate</param>
        /// <exception cref="EvaluationException">when function is undeclared</exception>
        public void Visit(FunctionCallExpression expression)
        {
            // Evaluate each expression passed as an argument and push calculated values onto the stack
            foreach (var expr in expression.Arguments)
                expr.Accept(this);

            int numArgs = expression.Arguments.Count;

            // Load args from stack
            var args = new double[numArgs];
            for (int i = 0; i < numArgs; ++i)
                args[i] = _numbers.Pop();
            
            var result = _context.TryCallFunction(expression.Name, args);
            if (result.HasValue)
                _numbers.Push(result.Value);
            else
                throw new EvaluationException($"Undeclared function {expression.Name}");
        }

        /// <summary>
        ///     Returns value from top of the stack.
        /// </summary>
        /// <returns>value from top of the stack</returns>
        public double Value => _numbers.Peek();
    }
}