using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CGraph;
using PropertyChanged;

namespace Calculator.GUI2
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        public string Expression { get; set; }
        public double Result { get; set; }
        public string Error { get; set; }
        public string ConstantName { get; set; }
        public string ConstantValue { get; set; }
        public ObservableCollection<Constant> Constants { get; } = new ObservableCollection<Constant>();

        [ImplementPropertyChanged]
        public class Constant
        {
            public string Identifier { get; set; }
            public double Value { get; set; }
            public override string ToString() => $"{Identifier} = {Value}";
        }

        private void OnExpressionChanged()
        {
            try
            {
                IEnumerable<Token> lexer = new Lexer(Expression);
                var parser = new CalculatorParser(lexer);
                var expression = parser.Parse();
                
                var context = new CalculatorEvaluatorContext();
                foreach (var constant in Constants)
                {
                    context.CreateConstant(constant.Identifier, constant.Value);
                }

                var visitor = new Evaluator(context);
                expression.Accept(visitor);
                Result = visitor.Value;
                Error = string.Empty;
            }
            catch (Exception ex)
            {
                Result = double.NaN;
                Error = $@"{ex.GetType().Name}: {ex.Message}";
            }
        }

        public ICommand AddConstantCommand => new RelayCommand(() =>
        {
            if (double.TryParse(ConstantValue, out double x))
            {
                Constants.Add(new Constant{Identifier = ConstantName, Value = x});
                ConstantName = string.Empty;
                ConstantValue = string.Empty;
            }
        });
    }
}
