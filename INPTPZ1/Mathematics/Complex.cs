using System;

namespace INPTPZ1.Mathematics
{
    public class Complex
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }
        
        private static readonly double TOLERANCE = 0.1f;

        public static readonly Complex Zero = new Complex {RealPart = 0, ImaginaryPart = 0};

        public override bool Equals(object obj)
        {
            if (obj is Complex complex)
                return Math.Abs(complex.RealPart - RealPart) < TOLERANCE && 
                       Math.Abs(complex.ImaginaryPart - ImaginaryPart) < TOLERANCE;
            return base.Equals(obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (RealPart.GetHashCode() * 397) ^ ImaginaryPart.GetHashCode();
            }
        }

        public Complex()
        {
            RealPart = 0;
            ImaginaryPart = 0;
        }

        public Complex(double realPart, double imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }

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
        
        public double Abs()
        {
            var exponent = 2;
            return Math.Sqrt( Math.Pow(RealPart, exponent) + Math.Pow(RealPart, exponent));
        }
        
        public double Angle()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public override string ToString()
        {
            return $"({RealPart:+#;-#;0.00} {ImaginaryPart:+#;-#;0.00}i)";
        }
    }
}