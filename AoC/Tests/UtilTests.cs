using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Util;

namespace Tests
{
    [TestClass]
    public class GCDTest
    {
        [TestMethod]
        public void GCD_SameNumbers_Input()
        {
            var result = Tools.GCD(4, 4);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void GCD_OneAnd31_One()
        {
            var result = Tools.GCD(1, 31);
            Assert.AreEqual(1, result);
        }


        [TestMethod]
        public void GCD_48And180_12()
        {
            var result = Tools.GCD(48, 180);
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void GCD_197And199_1()
        {
            var result = Tools.GCD(199, 197);
            Assert.AreEqual(1, result);
        }
    }
}
