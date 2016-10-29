namespace Calculator
{
    internal static class Precedences
    {
        public const int Addition = 1;
        public const int Substraction = 1;
        public const int Multiplication = 2;
        public const int Division = 2;
        public const int Exponentiation = 3;
        public const int Unary = 10;
        public const int ImplicitMultiplication = 20;
        public const int FunctionCall = 30;
    }
}
