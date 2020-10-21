using System;

namespace INPTPZ1.Parameters
{
    public class BitmapParameters
    {
        private int _width;
        
        public int Width
        {
            get => _width;
            set {
                _width = value;
                XStep = CalculateXStep();
            }
        }

        private int _height;
        
        public int Height {
            get => _height;
            set {
                _height = value;
                YStep = CalculateYStep();
            }
        }

        private double _xmax;
        public double XMax {
            get => _xmax;
            set {
                _xmax = value;
                XStep = CalculateXStep();
            }
        }

        private double _xmin;
        public double XMin {
            get => _xmin;
            set {
                _xmin = value;
                XStep = CalculateXStep();
            }
        }

        private double _ymin;
        public double YMin {
            get => _ymin;
            set {
                _ymin = value;
                YStep = CalculateYStep();
            }
        }

        private double _ymax;
        public double YMax {
            get => _ymax;
            set {
                _ymax = value;
                YStep = CalculateYStep();
            }
        }
        
        public string Output { get; set; }
        
        public double XStep { get; private set; }
        
        public double YStep { get; private set; }

        private double CalculateXStep() {
            return (XMax - XMin) / Width;
        }
        
        private double CalculateYStep() {
            return (YMax - YMin) / Height;
        }
        
        private static readonly int DefaultResolution = 200;
        
        private static readonly double DefaultNewtonsFractalsMin = -1;
        
        private static readonly double DefaultNewtonsFractalsMax = 1;

        private static readonly string DefaultFileLocation = "./out.png";
        
        public static BitmapParameters ParseParameters(string[] arguments)
        {
            BitmapParameters parameters = new BitmapParameters();

            parameters.Width = Int32.TryParse(arguments[0], out int width) ? width : DefaultResolution;
            parameters.Height = Int32.TryParse(arguments[1], out int height) ? height : DefaultResolution;

            ParseNewtonFractalsParameters(arguments, parameters);
            ParseOutputImageFile(arguments, parameters);
            
            return parameters;
        }
        private static void ParseNewtonFractalsParameters(string[] arguments, BitmapParameters parameters)
        {
            parameters.XMin = Double.TryParse(arguments[2], out double xmin) ? xmin : DefaultNewtonsFractalsMin;
            parameters.XMax = Double.TryParse(arguments[3], out double xman) ? xman : DefaultNewtonsFractalsMax;
            parameters.YMin = Double.TryParse(arguments[4], out double ymin) ? ymin : DefaultNewtonsFractalsMin;
            parameters.YMax = Double.TryParse(arguments[5], out double ymax) ? ymax : DefaultNewtonsFractalsMax;
        }

        private static void ParseOutputImageFile(string[] arguments, BitmapParameters parameters)
        {
            parameters.Output =  arguments[6] ?? DefaultFileLocation;
        }
    }
}