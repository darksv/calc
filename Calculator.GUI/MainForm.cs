using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Calculator;

namespace CalculatorGUI
{
    public partial class MainForm : Form
    {
        private readonly EvaluationContext _context = new CalculatorEvaluationContext();
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<Token> lexer = new Lexer(((TextBox) sender).Text);
                var parser = new CalculatorParser(lexer);
                var expression = parser.Parse();

                var visitor = new EvaluationVisitor(_context);
                expression.Accept(visitor);
                resultTextBox.Text = visitor.Value.ToString();
                errorTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                resultTextBox.Text = string.Empty;
                errorTextBox.Text = $@"{ex.GetType().FullName}: {ex.Message}";
            }
        }
    }
}