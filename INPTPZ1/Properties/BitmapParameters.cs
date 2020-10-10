using System;

namespace INPTPZ1.BitmapImage
{
    public struct BitmapParameters
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int XMax { get; set; }
        public int XMin { get; set; }
        public int YMin { get; set; }
        public int YMax { get; set; }
        public string Output { get; set; }
    }

    public class InvalidBitmapValueException : Exception
    {
        private int ParameterIndex { get; set; }
        private string ParameterValue{ get; set; }

        public InvalidBitmapValueException(int parameterIndex, string parameterValue) 
            : base("The value {0} for parameter at {1} index is incompatible.")
        {
            ParameterIndex = parameterIndex;
            ParameterValue = parameterValue;
        }
    }
}