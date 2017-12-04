using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Util;

namespace Tests
{
    [TestClass]
    public class AnagramTests
    {
        [TestMethod]
        public void EmptyAnagram()
        {
            var input = "";
            var other = "";

            Assert.IsTrue(input.isAnagram(other));
        }

        [TestMethod]
        public void SingleLetter()
        {
            var input = "a";
            var other = "a";

            Assert.IsTrue(input.isAnagram(other));
        }

        [TestMethod]
        public void SingleDifferentLetter()
        {
            var input = "a";
            var other = "b";

            Assert.IsFalse(input.isAnagram(other));
        }

        [TestMethod]
        public void DifferentLength()
        {
            var input = "a";
            var other = "aa";

            Assert.IsFalse(input.isAnagram(other));
        }

        [TestMethod]
        public void MultipleLetters()
        {
            var input = "cba";
            var other = "abc";

            Assert.IsTrue(input.isAnagram(other));
        }

    }
}
