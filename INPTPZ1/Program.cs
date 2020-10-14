using System;
using System.Collections.Generic;
using System.Drawing;
using INPTPZ1.BitmapImage;
using INPTPZ1.Mathematics;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
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

        private static readonly Color[] ColorPalette = {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private static readonly double NewtonsIterationToleration = 0.5;

        static void Main(string[] args)
        {
            BitmapParameters bitmapParameters = ParseBitmapParametersFromCommandLine(args);
            
            Polynomial polynomial = new Polynomial();
            polynomial.Complexes.AddRange(Initial());

            Bitmap bmp = CreateImage(bitmapParameters, polynomial);
            
            bmp.Save(bitmapParameters.Output ?? "./out.png");
            
            Console.ReadKey();
        }

        private static Bitmap CreateImage(BitmapParameters parameters, Polynomial polynomial) {
            List<Complex> roots = new List<Complex>();
            Bitmap bmp = new Bitmap(parameters.Width, parameters.Height);

            for (int xIndex = 0; xIndex < parameters.Width; xIndex++)
            {
                for (int yIndex = 0; yIndex < parameters.Height; yIndex++)
                {
                    double pixelX = parameters.XMin + yIndex * parameters.XStep;
                    double pixelY = parameters.YMin + xIndex * parameters.YStep;
                    

                    Complex pixelCoordinates = new Complex {
                        RealPart = pixelX,
                        ImaginaryPart = pixelY
                    };

                    int iteration = EquationNewtonsIteration(polynomial, ref pixelCoordinates);
                    int rootNumber = RootNumber(roots, pixelCoordinates);
                    
                    var color = PickColor(rootNumber, iteration);
                    bmp.SetPixel(xIndex, yIndex, color);
                }
            }

            return bmp;
        }

        private static Color PickColor(int rootNumber, int iteration)
        {
            Color color = ColorPalette[rootNumber % ColorPalette.Length];
            
            color = Color.FromArgb(color.R, color.G, color.B);
            
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iteration * 2), 255),
                Math.Min(Math.Max(0, color.G - iteration * 2), 255),
                Math.Min(Math.Max(0, color.B - iteration * 2), 255));
            
            return color;
        }

        private static int RootNumber(List<Complex> roots, Complex pixelCoordinates) {
            int rootNumber = 0;

            for (; rootNumber < roots.Count; rootNumber++)
            {
                if (pixelCoordinates.IsRoot(roots[rootNumber])) {
                    roots.Add(pixelCoordinates);
                    rootNumber = roots.Count;
                    break;
                }
            }
            
            return rootNumber;
        }

        private static int EquationNewtonsIteration(Polynomial polynomial, ref Complex pixelCoordinates)
        {
            Polynomial polynomialDerive = polynomial.Derive();
            int iteration = 0;
            
            for (int lower = 0; lower < NewtonUpperIteration; lower++, iteration++)
            {
                Complex polynomialEvaluation = polynomial.Evaluate(pixelCoordinates);
                Complex polynomialDeriveEvaluation = polynomialDerive.Evaluate(pixelCoordinates);
                
                Complex difference = polynomialEvaluation.Divide(polynomialDeriveEvaluation);
                
                pixelCoordinates = pixelCoordinates.Subtract(difference);
                
                if (difference.Pow() >= NewtonsIterationToleration) 
                    lower--;
            }

            return iteration;
        }

        private static BitmapParameters ParseBitmapParametersFromCommandLine(string[] arguments)
        {
            BitmapParameters parameters = new BitmapParameters();

            parameters.Width = Int32.Parse(arguments[0]);
            parameters.Height = Int32.Parse(arguments[1]);
            parameters.XMin = Int32.Parse(arguments[2]);
            parameters.XMax = Int32.Parse(arguments[3]);
            parameters.YMin = Int32.Parse(arguments[4]);
            parameters.YMax = Int32.Parse(arguments[5]);
            parameters.Output = arguments[6];

            return parameters;
        }
    }
}