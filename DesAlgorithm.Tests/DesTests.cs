using DesAlgorithm;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesAlgorithm.Tests
{
    [TestClass()]
    public class DesTests
    {
        private readonly Logger logger;
        private readonly Des des;

        public DesTests()
        {
            logger = new Logger();
            des = new Des(logger);
        }

        [TestMethod()]
        public void SetParityBitsTest()
        {
            byte[] shortKey = { 0x0E, 0x66, 0x49, 0x9E, 0xAD, 0x83, 0x39 };
            byte[] fullKey = { 0x0E, 0x32, 0x92, 0x32, 0xEA, 0x6D, 0x0D, 0x73 };
            Assert.IsTrue(fullKey.SequenceEqual(des.SetParityBits(shortKey)));
        }

        [TestMethod()]
        public void GetPermutedKeyTest()
        {
            byte[] originalKey = { 0x13, 0x34, 0x57, 0x79, 0x9B, 0xBC, 0xDF, 0xF1 };
            byte[] expectedKey = { 0xF0, 0xCC, 0xAA, 0xF5, 0x56, 0x67, 0x8F };
            Assert.IsTrue(expectedKey.SequenceEqual(des.GetPermutedKey(originalKey)));
        }
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}