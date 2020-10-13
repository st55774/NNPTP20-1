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

        static void Main(string[] args)
        {
            var bitmapParameters = ParseBitmapParametersFromCommandLine(args);
            
            Polynomial polynomial = new Polynomial();
            polynomial.Complexes.AddRange(Initial());

            Bitmap bmp = CreateImage(bitmapParameters, polynomial);
            
            bmp.Save(bitmapParameters.Output ?? "./out.png");
            Console.ReadKey();
        }

        private static Bitmap CreateImage(BitmapParameters parameters, Polynomial polynomial) {
            List<Complex> roots = new List<Complex>();
            Bitmap bmp = new Bitmap(parameters.Width, parameters.Height);

            int maxid1 = 0;
            for (int xIndex = 0; xIndex < parameters.Width; xIndex++)
            {
                for (int yIndex = 0; yIndex < parameters.Height; yIndex++)
                {
                    double pixelX = parameters.XMin + yIndex * parameters.XStep;
                    double pixelY = parameters.YMin + xIndex * parameters.YStep;
                    

                    Complex ox = new Complex {
                        RealPart = pixelX,
                        ImaginaryPart = pixelY
                    };

                    // find solution of equation using newton's iteration
                    var it = EquationNewtonsIteration(polynomial, ref ox);

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w < roots.Count; w++)
                    {
                        if (Math.Pow(ox.RealPart - roots[w].RealPart, 2) +
                            Math.Pow(ox.ImaginaryPart - roots[w].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }

                    if (!known)
                    {
                        roots.Add(ox);
                        id = roots.Count;
                        maxid1 = id + 1;
                    }

                    // colorize pixel according to root number
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    var vv = ColorPalette[id % ColorPalette.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int) it * 2), 255),
                        Math.Min(Math.Max(0, vv.G - (int) it * 2), 255),
                        Math.Min(Math.Max(0, vv.B - (int) it * 2), 255));
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    bmp.SetPixel(yIndex, xIndex, vv);
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
                }
            }

            return bmp;
        }

        private static int EquationNewtonsIteration(Polynomial polynomial, ref Complex ox)
        {
            int it = 0;
            Polynomial polynomialDerive = polynomial.Derive();
            
            for (int q = 0; q < NewtonUpperIteration; q++)
            {
                var diff = polynomial.Evaluate(ox).Divide(polynomialDerive.Evaluate(ox));
                ox = ox.Subtract(diff);

                //Console.WriteLine($"{q} {ox} -({diff})");
                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                {
                    q--;
                }

                it++;
            }

            return it;
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