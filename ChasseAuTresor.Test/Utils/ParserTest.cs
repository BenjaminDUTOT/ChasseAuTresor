using ChasseAuTresor.Model.AdventurerModel;
using NUnit.Framework;
using ChasseAuTresor.Utils;
using System;

namespace ChasseAuTresorTest.Utils
{
    public class Tests
    {
        [Test]
        public void ParseIntegerOk()
        {
            int result = Parser.ParseInteger("5");
            Assert.That(5.Equals(result));
        }

        [Test]
        public void ParseIntegerNull()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseInteger(null));
        }

        [Test]
        public void ParseIntegerLetter()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseInteger("a"));
        }

        [Test]
        public void ParseOrientationOkSmallLetrer()
        {
            Orientation result = Parser.ParseOrientation("o");
            Assert.That(Orientation.O.Equals(result));
        }

        [Test]
        public void ParseOrientationOkBigLetrer()
        {
            Orientation result = Parser.ParseOrientation("O");
            Assert.That(Orientation.O.Equals(result));
        }

        [Test]
        public void ParseOrientationNull()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseOrientation(null));
        }

        [Test]
        public void ParseOrientationInvalidLetter()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseOrientation("a"));
        }
    }
}