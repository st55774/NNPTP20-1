using INPTPZ1.Mathematics;
using NewtonFractals.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPTPZ1Tests
{
    [TestClass]
    public class ComplexTests
    {

        [TestMethod]
        public void AddTest()
        {
            Complex expected = new Complex {
                RealPart = 11,
                ImaginaryPart = 22
            };
            
            Complex a = new Complex {
                RealPart = 10,
                ImaginaryPart = 20
            };
            Complex b = new Complex {
                RealPart = 1,
                ImaginaryPart = 2
            };
            Complex result = a.Add(b);
            
            Assert.AreEqual(expected, result);
        }
    }
}


