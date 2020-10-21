using System;
using System.Drawing;
using INPTPZ1.NewtonFractals;
using INPTPZ1.Parameters;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BitmapParameters bitmapParameters = BitmapParameters.ParseParameters(args);

            NewtonFractalsManager newtonFractalsManager = new NewtonFractalsManager();
            Bitmap bmp = newtonFractalsManager.CreateImage(bitmapParameters);

            bmp.Save(bitmapParameters.Output ?? "./out.png");
        }
    }
}