using Microsoft.VisualStudio.TestTools.UnitTesting;
using INPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex a = new Complex()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            Complex b = new Complex()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            Complex actual = a.Add(b);
            Complex shouldBe = new Complex()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(shouldBe, actual);
        }
    }
}


