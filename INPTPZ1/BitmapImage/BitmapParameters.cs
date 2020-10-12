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

        private int _xmax;
        public int XMax {
            get => _xmax;
            set {
                _xmax = value;
                XStep = CalculateXStep();
            }
        }

        private int _xmin;
        public int XMin {
            get => _xmin;
            set {
                _xmin = value;
                XStep = CalculateXStep();
            }
        }

        private int _ymin;
        public int YMin {
            get => _ymin;
            set {
                _ymin = value;
                YStep = CalculateYStep();
            }
        }

        private int _ymax;
        public int YMax {
            get => _ymax;
            set {
                _ymax = value;
                YStep = CalculateYStep();
            }
        }
        
        public string Output { get; set; }
        
        public int XStep { get; private set; }
        
        public int YStep { get; private set; }

        private int CalculateXStep() {
            return (XMax - XMin) / Width;
        }
        
        private int CalculateYStep() {
            return (YMax - YMin) / Height;
        }
    }
}