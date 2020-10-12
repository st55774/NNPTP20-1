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
        public static List<Complex> Initial()
        {
            return new List<Complex>
            {
                new Complex {RealPart = 1},
                Complex.Zero, Complex.Zero,
                new Complex {RealPart = 1}
            };
        }

        public static readonly Color[] InitialColors = new[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            var bitmapParameters = ParseBitmapParametersFromCommandLine(args);
            Bitmap bmp = new Bitmap(bitmapParameters.Width, bitmapParameters.Height);

            Polynomial polynomial = new Polynomial();
            polynomial.Complexes.AddRange(Initial());
            Polynomial polynomialDerive = polynomial.Derive();

            List<Complex> koreny = new List<Complex>();

            // TODO: cleanup!!!
            // for every pixel in image...
            CreateImage(bitmapParameters, polynomial, polynomialDerive, koreny, bmp, out var i1);

            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color c = bmp.GetPixel(j, i);
            //        int nv = (int)Math.Floor(c.R * (255.0 / maxid));
            //        bmp.SetPixel(j, i, Color.FromArgb(nv, nv, nv));
            //    }
            //}

            bmp.Save(bitmapParameters.Output ?? "../../../out.png");
            Console.ReadKey();
        }

        private static void CreateImage(BitmapParameters bitmapParameters1, Polynomial polynomial1,
            Polynomial polynomialDerive1, List<Complex> complexes, Bitmap bitmap, out int i)
        {
            int maxid1 = 0;
            for (i = 0; i < bitmapParameters1.Width; i++)
            {
                for (int j = 0; j < bitmapParameters1.Height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = bitmapParameters1.YMin + i * bitmapParameters1.YStep;
                    double x = bitmapParameters1.XMin + j * bitmapParameters1.XStep;

                    Complex ox = new Complex
                    {
                        RealPart = x,
                        ImaginaryPart = y
                    };

                    if (ox.RealPart == 0)
                        ox.RealPart = 0.0001;
                    if (ox.ImaginaryPart == 0)
                        ox.ImaginaryPart = 0.0001f;

                    //Console.WriteLine(ox);

                    // find solution of equation using newton's iteration
                    int it = 0;
                    for (int q = 0; q < 30; q++)
                    {
                        var diff = polynomial1.Eval(ox).Divide(polynomialDerive1.Eval(ox));
                        ox = ox.Subtract(diff);

                        //Console.WriteLine($"{q} {ox} -({diff})");
                        if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                        {
                            q--;
                        }

                        it++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w < complexes.Count; w++)
                    {
                        if (Math.Pow(ox.RealPart - complexes[w].RealPart, 2) +
                            Math.Pow(ox.ImaginaryPart - complexes[w].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }

                    if (!known)
                    {
                        complexes.Add(ox);
                        id = complexes.Count;
                        maxid1 = id + 1;
                    }

                    // colorize pixel according to root number
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    var vv = InitialColors[id % InitialColors.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int) it * 2), 255),
                        Math.Min(Math.Max(0, vv.G - (int) it * 2), 255),
                        Math.Min(Math.Max(0, vv.B - (int) it * 2), 255));
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    bitmap.SetPixel(j, i, vv);
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
                }
            }
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