using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorEvaluationContext : EvaluationContext
    {
        public CalculatorEvaluationContext()
        {
            CreateFunction("sin", Math.Sin);
            CreateFunction("cos", Math.Cos);
            CreateFunction("tan", Math.Tan);
            CreateFunction("cot", x => Math.Atan2(1, x));

            CreateFunction("asin", Math.Asin);
            CreateFunction("acos", Math.Acos);
            CreateFunction("atan", Math.Atan);
            CreateFunction("acot", x => Math.PI / 2.0 - Math.Atan(x));

            CreateFunction("exp", Math.Exp);
            CreateFunction("sqrt", Math.Sqrt);
            CreateFunction("avg", args => args.Sum() / args.Length);

            CreateConstant("pi", Math.PI);
            CreateConstant("e", Math.E);
        }
    }
}
