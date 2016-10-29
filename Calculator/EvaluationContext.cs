using System;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    ///     Class responsible for storing context data like constants and functions, necessary to evaluate expression.
    /// </summary>
    public class EvaluationContext
    {
        /// <summary>
        ///     Delegate intended to be an function called during evaluation.
        /// </summary>
        /// <param name="args">arguments for the function</param>
        /// <returns>result returned by the function</returns>
        public delegate double CustomFunc(params double[] args);

        /// <summary>
        ///     All constants available in the context.
        /// </summary>
        private readonly Dictionary<string, double> _constants = new Dictionary<string, double>();

        /// <summary>
        ///     All functions available in the context.
        /// </summary>
        private readonly Dictionary<string, CustomFunc> _functions = new Dictionary<string, CustomFunc>();
        
        /// <summary>
        ///     Sets value of a constant with given name.
        /// </summary>
        /// <param name="name">name of the constant</param>
        /// <param name="value">value to store in constant</param>
        public void CreateConstant(string name, double value)
        {
            _constants.Add(name, value);
        }

        /// <summary>
        ///     Creates new function with given name and body which accepts any number of arguments.
        /// </summary>
        /// <param name="name">name of the function</param>
        /// <param name="func">body of the function</param>
        public void CreateFunction(string name, CustomFunc func)
        {
            _functions.Add(name, func);
        }

        /// <summary>
        ///     Creates new function with given name and body which accepts only one argument.
        /// </summary>
        /// <param name="name">name of the function</param>
        /// <param name="func">body of the function</param>
        public void CreateFunction(string name, Func<double, double> func)
        {
            CreateFunction(name, args =>
            {
                if (args.Length < 1)
                    throw new EvaluationException($"Too few arguments for function {name}");
                else if (args.Length > 1)
                    throw new EvaluationException($"Too many arguments for function {name}");
                return func(args[0]);
            });
        }

        /// <summary>
        ///     Tries to call named function with given arguments.
        /// </summary>
        /// <param name="name">name of the function</param>
        /// <param name="args">arguments to pass to the function</param>
        /// <exception cref="EvaluationException">when arguments are invalid</exception>
        /// <returns>value returned by function or null if function is not defined</returns>
        public double? TryCallFunction(string name, params double[] args)
        {
            CustomFunc customFunc;
            return _functions.TryGetValue(name, out customFunc) ? (double?)customFunc(args) : null;
        }

        /// <summary>
        ///     Tries to get value of given constant.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>value stored in constant or null if such constant is not defined</returns>
        public double? TryGetConstant(string name)
        {
            double value;
            return _constants.TryGetValue(name, out value) ? (double?)value : null;
        }
    }
}
