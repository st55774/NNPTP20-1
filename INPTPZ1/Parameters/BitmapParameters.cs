using System;

namespace INPTPZ1.Parameters
{
    public class BitmapParameters
    {
        private int _width = DefaultResolution;
        
        public int Width
        {
            get => _width;
            set {
                _width = value;
                XStep = CalculateXStep();
            }
        }

        private int _height = DefaultResolution;
        
        public int Height {
            get => _height;
            set {
                _height = value;
                YStep = CalculateYStep();
            }
        }

        private double _xmax = DefaultNewtonsFractalsMax;
        public double XMax {
            get => _xmax;
            set {
                _xmax = value;
                XStep = CalculateXStep();
            }
        }

        private double _xmin = DefaultNewtonsFractalsMin;
        public double XMin {
            get => _xmin;
            set {
                _xmin = value;
                XStep = CalculateXStep();
            }
        }

        private double _ymin = DefaultNewtonsFractalsMin;
        public double YMin {
            get => _ymin;
            set {
                _ymin = value;
                YStep = CalculateYStep();
            }
        }

        private double _ymax = DefaultNewtonsFractalsMax;
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
        
        private static readonly int WidthParameterIndex = 0;
        
        private static readonly int HeightParameterIndex = 1;
        
        private static readonly int XMinParameterIndex = 2;
        
        private static readonly int XMaxParameterIndex = 3;
        
        private static readonly int YMinParameterIndex = 4;
        
        private static readonly int YMaxParameterIndex = 5;
        
        private static readonly int ImageParameterIndex = 6;

        public static BitmapParameters ParseParameters(string[] arguments)
        {
            BitmapParameters parameters = new BitmapParameters();

            if(arguments.Length > WidthParameterIndex)
                parameters.Width = Int32.TryParse(arguments[WidthParameterIndex], out int width) ? width : DefaultResolution;
            if(arguments.Length > HeightParameterIndex)
                parameters.Height = Int32.TryParse(arguments[HeightParameterIndex], out int height) ? height : DefaultResolution;

            ParseNewtonFractalsParameters(arguments, parameters);
            ParseOutputImageFile(arguments, parameters);
            
            return parameters;
        }
        private static void ParseNewtonFractalsParameters(string[] arguments, BitmapParameters parameters)
        {
            if(arguments.Length > XMinParameterIndex)
                parameters.XMin = Double.TryParse(arguments[XMinParameterIndex], out double xmin) ? xmin : DefaultNewtonsFractalsMin;
            
            if(arguments.Length > XMaxParameterIndex)
                parameters.XMax = Double.TryParse(arguments[XMaxParameterIndex], out double xman) ? xman : DefaultNewtonsFractalsMax;
            
            if(arguments.Length > YMinParameterIndex)
                parameters.YMin = Double.TryParse(arguments[YMinParameterIndex], out double ymin) ? ymin : DefaultNewtonsFractalsMin;
            
            if(arguments.Length > YMaxParameterIndex)
                parameters.YMax = Double.TryParse(arguments[YMaxParameterIndex], out double ymax) ? ymax : DefaultNewtonsFractalsMax;
        }

        private static void ParseOutputImageFile(string[] arguments, BitmapParameters parameters)
        {
            if (arguments.Length > ImageParameterIndex)
                parameters.Output = arguments[ImageParameterIndex];
        }
    }
}