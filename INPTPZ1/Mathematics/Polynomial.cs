using System.Collections.Generic;

namespace INPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<Complex> Complexes { get; } = new List<Complex>();

        public Polynomial Derive() {
            Polynomial derived = new Polynomial();
            int index = 1;
            
            Complexes.GetRange(index, Complexes.Count).ForEach(current => {
                Complex multiple = new Complex {RealPart = index++, ImaginaryPart = 0};
                derived.Complexes.Add(current.Multiply(multiple));
            });

            return derived;
        }

        public Complex Eval(Complex x)
        {
            Complex s = Complex.Zero;
            for (int i = 0; i < Complexes.Count; i++)
            {
                Complex coef = Complexes[i];
                Complex bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coef = coef.Multiply(bx);
                }

                s = s.Add(coef);
            }

            return s;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Complexes.Count; i++)
            {
                s += Complexes[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        s += "x";
                    }
                }
                s += " + ";
            }
            return s;
        }
    }
}