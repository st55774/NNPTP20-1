using System.Collections.Generic;
using System.Text;
using INPTPZ1.Mathematics;

namespace NewtonFractals.Mathematics
{
    public class Polynomial
    {
        public List<Complex> Complexes { get; } = new List<Complex>();

        public Polynomial Derive() {
            Polynomial derived = new Polynomial();
            int index = 1;

            if (index < Complexes.Count)
                Complexes.GetRange(index, Complexes.Count-1).ForEach(current => {
                    Complex multiple = new Complex {RealPart = index++, ImaginaryPart = 0};
                    derived.Complexes.Add(current.Multiply(multiple));
                });
            
            return derived;
        }

        public Complex Evaluate(Complex evaluation)
        {
            Complex evaluated = Complex.Zero;
            int level = 0;
            
            Complexes.ForEach(coefficient => {
                evaluated = evaluated.Add(level > 0 ? CalculateCoefficientToAdd(coefficient, evaluation,level) : coefficient);
                level++;
            });

            return evaluated;
        }

        private static Complex CalculateCoefficientToAdd(Complex coefficient, Complex evaluation, int level)
        {
            Complex evaluationIndex = evaluation;
            
            for (int i = 0; i < level - 1; i++)
                evaluationIndex = evaluationIndex.Multiply(evaluation);
            
            return coefficient.Multiply(evaluationIndex);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            int level = 0;
            
            Complexes.ForEach(complex =>
            {
                builder.Append(complex);
                builder.Append(level > 0 ? $"X^{level + 1} + " : " + ");
                level++;
            });

            return builder.ToString();
        }
    }
}