using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorEvaluationContext : EvaluationContext
    {
        public CalculatorEvaluationContext()
        {
            CreateFunction("sin", args => Math.Sin(args[0]));
            CreateFunction("cos", args => Math.Cos(args[0]));
            CreateFunction("tan", args => Math.Tan(args[0]));
            CreateFunction("cot", args => Math.Atan2(1, args[0]));

            CreateFunction("asin", args => Math.Asin(args[0]));
            CreateFunction("acos", args => Math.Acos(args[0]));
            CreateFunction("atan", args => Math.Atan(args[0]));
            CreateFunction("acot", args => Math.PI / 2.0 - Math.Atan(args[0]));

            CreateFunction("exp", args => Math.Exp(args[0]));
            CreateFunction("sqrt", args => Math.Sqrt(args[0]));
            CreateFunction("avg", args => args.Sum() / args.Length);

            CreateConstant("pi", Math.PI);
            CreateConstant("e", Math.E);
        }
    }
}
