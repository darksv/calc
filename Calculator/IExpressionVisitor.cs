using Calculator.Expressions;

namespace Calculator
{
    /// <summary>
    ///     Interface shared by all expression visitors.
    /// </summary>
    public interface IExpressionVisitor
    {
        /// <summary>
        ///     Visits an unary expression.
        /// </summary>
        /// <param name="expression">expression to visit</param>
        void Visit(UnaryExpression expression);

        /// <summary>
        ///     Visits a binary expression.
        /// </summary>
        /// <param name="expression">expression to visit</param>
        void Visit(BinaryExpression expression);

        /// <summary>
        ///     Visits a number expression.
        /// </summary>
        /// <param name="expression">expression to visit</param>
        void Visit(NumberExpression expression);

        /// <summary>
        ///     Visits a name expression.
        /// </summary>
        /// <param name="expression">expression to visit</param>
        void Visit(IdentifierExpression expression);

        /// <summary>
        ///     Visits a function call expression.
        /// </summary>
        /// <param name="expression">expression to visit</param>
        void Visit(FunctionCallExpression expression);
    }
}