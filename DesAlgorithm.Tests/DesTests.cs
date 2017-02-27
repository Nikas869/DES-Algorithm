using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesAlgorithm.Tests
{
    [TestClass()]
    public class DesTests
    {
        [TestMethod()]
        public void SetParityBitsTest()
        {
            Logger logger = new Logger();
            Des des = new Des(logger);

            byte[] shortKey = { 0x0E, 0x66, 0x49, 0x9E, 0xAD, 0x83, 0x39 };
            byte[] fullKey = { 0x0E, 0x32, 0x92, 0x32, 0xEA, 0x6D, 0x0D, 0x73 };
            Assert.IsTrue(fullKey.SequenceEqual(des.SetParityBits(shortKey)));
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