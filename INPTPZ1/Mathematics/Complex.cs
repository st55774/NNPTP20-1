using System;

namespace INPTPZ1.Mathematics
{
    public class Complex
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }
        
        private const double Tolerance = 0.1f;

        public static readonly Complex Zero = new Complex {RealPart = 0, ImaginaryPart = 0};
        
        private const double RootToleration = 0.01;

        private const int Exponent = 2;

        public Complex Add(Complex adder)
        {
            return new Complex
            {
                RealPart = RealPart + adder.RealPart,
                ImaginaryPart = ImaginaryPart + adder.ImaginaryPart
            };
        }
        public Complex Subtract(Complex reducer)
        {
            return new Complex
            {
                RealPart = RealPart - reducer.RealPart,
                ImaginaryPart = ImaginaryPart - reducer.ImaginaryPart
            };
        }
        
        public Complex Multiply(Complex multiple)
        {
            return new Complex
            {
                RealPart = RealPart * multiple.RealPart - ImaginaryPart * multiple.ImaginaryPart,
                ImaginaryPart = RealPart * multiple.ImaginaryPart + ImaginaryPart * multiple.RealPart
            };
        }
        
        public Complex Divide(Complex divisor)
        {
            Complex dividendResult = Multiply(new Complex { RealPart = divisor.RealPart, ImaginaryPart = -divisor.ImaginaryPart });
            double divisorResult = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;

            return new Complex 
            {
                RealPart = dividendResult.RealPart / divisorResult,
                ImaginaryPart = dividendResult.ImaginaryPart / divisorResult
            };
        }

        public double Pow()
        {
            return Math.Pow(RealPart, Exponent) + Math.Pow(ImaginaryPart, Exponent);
        }

        public override string ToString()
        {
            return $"({RealPart:+#;-#;0.00} {ImaginaryPart:+#;-#;0.00}i)";
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Complex complex)
                return Math.Abs(complex.RealPart - RealPart) < Tolerance && 
                       Math.Abs(complex.ImaginaryPart - ImaginaryPart) < Tolerance;
            return base.Equals(obj);
        }

        public bool Equals(Complex other)
        {
            return Math.Pow(RealPart - other.RealPart, Exponent) + 
                Math.Pow(ImaginaryPart - other.ImaginaryPart, Exponent) <= RootToleration;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RealPart.GetHashCode() * 397) ^ ImaginaryPart.GetHashCode();
            }
        }
    }
}