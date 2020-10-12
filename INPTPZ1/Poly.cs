using System.Collections.Generic;

namespace INPTPZ1.Mathematics
{
    class Poly
    {
        public List<Complex> Coe { get; set; }

        public Poly() => Coe = new List<Complex>();

        public Poly Derive()
        {
            Poly p = new Poly();
            for (int i = 1; i < Coe.Count; i++)
            {
                p.Coe.Add(Coe[i].Multiply(new Complex() { RealPart = i }));
            }

            return p;
        }

        public Complex Eval(Complex x)
        {
            Complex s = Complex.Zero;
            for (int i = 0; i < Coe.Count; i++)
            {
                Complex coef = Coe[i];
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
            for (int i = 0; i < Coe.Count; i++)
            {
                s += Coe[i];
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