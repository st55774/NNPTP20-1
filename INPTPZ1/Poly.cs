using System.Collections.Generic;

namespace INPTPZ1.Mathematics
{
    class Poly
    {
        public List<Cplx> Coe { get; set; }

        public Poly() => Coe = new List<Cplx>();

        public Poly Derive()
        {
            Poly p = new Poly();
            for (int i = 1; i < Coe.Count; i++)
            {
                p.Coe.Add(Coe[i].Multiply(new Cplx() { Re = i }));
            }

            return p;
        }

        public Cplx Eval(Cplx x)
        {
            Cplx s = Cplx.Zero;
            for (int i = 0; i < Coe.Count; i++)
            {
                Cplx coef = Coe[i];
                Cplx bx = x;
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