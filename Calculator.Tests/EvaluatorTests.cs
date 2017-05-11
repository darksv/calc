using System;
using Calculator;
using Calculator.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
    [TestClass]
    public class EvaluatorTests
    {
        [TestMethod]
        public void Evaluate_NumberExpression_Calculated()
        {
            var evaluator = new Evaluator(new EvaluatorContext());
            var expression = new NumberExpression(123);

            var actual = evaluator.Evaluate(expression);

            Assert.AreEqual(123, actual);
        }

        [TestMethod]
        public void Evaluate_Identifier_Calculated()
        {
            var expression = new IdentifierExpression("pi");
            var context = new EvaluatorContext();
            context.CreateConstant("pi", Math.PI);
            var evaluator = new Evaluator(context);

            var actual = evaluator.Evaluate(expression);

            Assert.AreEqual(Math.PI, actual);
        }

        [TestMethod]
        public void Evaluate_BinaryOperation_Calculated()
        {
            var evaluator = new Evaluator(new EvaluatorContext());
            var expression = new BinaryOperatorExpression(TokenKind.Plus, new NumberExpression(123), new NumberExpression(-1.37));

            var actual = evaluator.Evaluate(expression);

            Assert.AreEqual(121.63, actual);
        }

        [TestMethod]
        public void Evaluate_UnaryOperation_Calculated()
        {
            var evaluator = new Evaluator(new EvaluatorContext());
            var expression = new UnaryOperatorExpression(TokenKind.Minus, new NumberExpression(-123));

            var actual = evaluator.Evaluate(expression);

            Assert.AreEqual(123, actual);
        }

        [TestMethod]
        public void Evaluate_FunctionCall_Calculated()
        {
            var expression = new FunctionCallExpression("exp", new IExpression[] { new NumberExpression(1) });
            var context = new EvaluatorContext();
            context.CreateFunction("exp", Math.Exp);
            var evaluator = new Evaluator(context);

            var actual = evaluator.Evaluate(expression);

            Assert.AreEqual(Math.E, actual);
        }
    }
}
