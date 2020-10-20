namespace INPTPZ1.BitmapImage
{
    public  struct BitmapParameters
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
    }
}