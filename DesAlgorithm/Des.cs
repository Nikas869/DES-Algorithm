using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;

namespace DesAlgorithm
{
    public class Des
    {
        private readonly ILogger logger;
        public Des(ILogger logger)
        {
            this.logger = logger;
        }

        public string GenerateKey()
        {
            byte[] keyByteArray;
            do
            {
                byte[] randomByteArray = GetRandomBytes(7);
                keyByteArray = SetParityBits(randomByteArray);
            } while (false); 

            return BitConverter.ToString(keyByteArray).Replace("-", String.Empty);
        }

        private byte[] SetParityBits(byte[] keyByteArray)
        {
            logger.Log($"Generated key (7 bytes): {BitConverter.ToString(keyByteArray)}");
            // Reverse
            keyByteArray = keyByteArray.Select(b => (byte)((b * 0x0202020202 & 0x010884422010) % 1023)).ToArray();
            logger.Log($"Reversed key (7 bytes): {BitConverter.ToString(keyByteArray)}");
            var keyBitArray = new BitArray(keyByteArray);
            var resultBitArray = new BitArray(64);
            int counter = 0;
            for (int i = 0, j = 0; i < keyBitArray.Length; i++, j++)
            {
                if ((i + 1) % 8 == 0 && i != 0)
                {
                    resultBitArray[j] = counter % 2 == 0;
                    counter = keyBitArray[i] ? 1 : 0;
                    j++;
                }
                else if (keyBitArray[i])
                {
                    resultBitArray[j] = true;
                    counter++;
                }
                else
                {
                    resultBitArray[j] = false;
                }
            }
            var result = new byte[8];
            resultBitArray.CopyTo(result, 0);
            // Reverse
            result = result.Select(b => (byte)((b * 0x0202020202 & 0x010884422010) % 1023)).ToArray();
            return result;
        }

        private static byte[] GetRandomBytes(int length)
        {
            byte[] result = new byte[length];
            RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();

            randomNumberGenerator.GetBytes(result);

            return result;
        }
    }
}