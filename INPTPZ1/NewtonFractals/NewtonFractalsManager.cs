using System;
using System.Collections.Generic;
using System.Drawing;
using INPTPZ1.Mathematics;
using INPTPZ1.Parameters;
using NewtonFractals.Mathematics;

namespace INPTPZ1.NewtonFractals
{
    public class NewtonFractalsManager
    {
        private readonly Color[] _colorPalette =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private readonly double NewtonsIterationToleration = 0.5;
        
        private readonly int _colorUpperValue = 255;
        
        private static readonly int _colorLowerValue = 0;
        
        private readonly int _colorMultiplier = 2;

        private const int NewtonUpperIteration = 30;

        private static List<Complex> Initial()
        {
            return new List<Complex>
            {
                new Complex {RealPart = 1},
                Complex.Zero, Complex.Zero,
                new Complex {RealPart = 1}
            };
        }

        public Bitmap CreateImage(BitmapParameters parameters)
        {
            List<Complex> roots = new List<Complex>();
            Bitmap bmp = new Bitmap(parameters.Width, parameters.Height);
            
            Polynomial polynomial = new Polynomial();
            polynomial.Complexes.AddRange(Initial());

            for (int xIndex = 0; xIndex < parameters.Width; xIndex++)
            {
                for (int yIndex = 0; yIndex < parameters.Height; yIndex++)
                {
                    double pixelX = parameters.XMin + yIndex * parameters.XStep;
                    double pixelY = parameters.YMin + xIndex * parameters.YStep;


                    Complex pixelCoordinates = new Complex
                    {
                        RealPart = pixelX,
                        ImaginaryPart = pixelY
                    };

                    int iteration = EquationNewtonsIteration(polynomial, ref pixelCoordinates);
                    int rootNumber = RootNumber(roots, pixelCoordinates);

                    Color color = PickColor(rootNumber, iteration);
                    bmp.SetPixel(yIndex, xIndex, color);
                }
            }

            return bmp;
        }

        private Color PickColor(int rootNumber, int iteration)
        {
            Color color = _colorPalette[rootNumber % _colorPalette.Length];

            color = Color.FromArgb(Math.Min(Math.Max(_colorLowerValue, color.R - iteration * _colorMultiplier), _colorUpperValue),
                Math.Min(Math.Max(_colorLowerValue, color.G - iteration * _colorMultiplier), _colorUpperValue),
                Math.Min(Math.Max(_colorLowerValue, color.B - iteration * _colorMultiplier), _colorUpperValue));

            return color;
        }

        private int RootNumber(List<Complex> roots, Complex pixelCoordinates)
        {
            for (int i = 0; i < roots.Count; i++)
                if (pixelCoordinates.IsRoot(roots[i]))
                    return i;
            
            roots.Add(pixelCoordinates);
            return roots.Count;
        }

        private int EquationNewtonsIteration(Polynomial polynomial, ref Complex pixelCoordinates)
        {
            Polynomial polynomialDerive = polynomial.Derive();
            int iteration = 0;

            for (int limit = 0; limit < NewtonUpperIteration; limit++, iteration++)
            {
                Complex polynomialEvaluation = polynomial.Evaluate(pixelCoordinates);
                Complex polynomialDeriveEvaluation = polynomialDerive.Evaluate(pixelCoordinates);

                Complex difference = polynomialEvaluation.Divide(polynomialDeriveEvaluation);

                pixelCoordinates = pixelCoordinates.Subtract(difference);

                if (difference.Pow() >= NewtonsIterationToleration)
                    limit--;
            }

            return iteration;
        }
    }
}