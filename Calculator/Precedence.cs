using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal static class Precedence
    {
        public const int Addition = 1;
        public const int Substraction = 1;
        public const int Multiplication = 2;
        public const int Division = 2;
        public const int Unary = 10;
    }
}
