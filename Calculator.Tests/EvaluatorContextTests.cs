using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
    [TestClass]
    public class EvaluatorContextTests
    {
        [TestMethod]
        public void HasConstant_ExistentConstant_ReturnedTrue()
        {
            var context = new EvaluatorContext();
            context.CreateConstant("c", 13.37);

            var actual = context.HasConstant("c");
             
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasConstant_InexistentConstant_ReturnedFalse()
        {
            var context = new EvaluatorContext();
            context.CreateConstant("a", 23.37);

            var actual = context.HasConstant("b");

            Assert.IsFalse(actual);
        }
        
        [TestMethod]
        public void GetConstant_ExistentConstant_GotValue()
        {
            var context = new EvaluatorContext();
            context.CreateConstant("ddd", 13.37);

            var actual = context.GetConstant("ddd");

            Assert.AreEqual(13.37, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(EvaluatorException), "Undefined constant 'ddd'")]
        public void GetConstant_InexistentConstant_ExceptionThrown()
        {
            var context = new EvaluatorContext();

            context.GetConstant("ddd");
        }

        [TestMethod]
        public void CallFunction_ExistentFunction_GotReturnedValue()
        {
            var context = new EvaluatorContext();
            context.CreateFunction("fun", x => x + 1);

            var actual = context.CallFunction("fun", 2);

            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(EvaluatorException), "Undeclared function 'fun'")]
        public void CallFunction_InexistentFunction_GotNull()
        {
            var context = new EvaluatorContext();

            context.CallFunction("fun", 2);
        }

        [TestMethod]
        [ExpectedException(typeof(EvaluatorException), "Function 'f' expects 1 argument, got 0")]
        public void CallFunction_TooFewArguments_ExceptionThrown()
        {
            var context = new EvaluatorContext();
            context.CreateFunction("f", x => x * (x + 1));

            context.CallFunction("f");
        }

        [TestMethod]
        [ExpectedException(typeof(EvaluatorException), "Function g expects 1 argument, got 2")]
        public void CallFunction_TooManyArguments_ExceptionThrown()
        {
            var context = new EvaluatorContext();
            context.CreateFunction("g", x => x / (x + 1));

            context.CallFunction("g", 1, 2);
        }
    }
}
