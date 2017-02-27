using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;

namespace DesAlgorithm
{
    public class Des
    {
        private readonly ArrayList weakKeys = new ArrayList
        {
            new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 },
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE },
            new byte[] { 0x1F, 0x1F, 0x1F, 0x1F, 0x0E, 0x0E, 0x0E, 0x0E },
            new byte[] { 0xE0, 0xE0, 0xE0, 0xE0, 0xF1, 0xF1, 0xF1, 0xF1 }
        };
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
            } while (weakKeys.Contains(keyByteArray)); 

            return BitConverter.ToString(keyByteArray).Replace("-", String.Empty);
        }

        public byte[] SetParityBits(byte[] keyByteArray)
        {
            logger.Log($"Generated key (7 bytes): {BitConverter.ToString(keyByteArray)}");

            // Reverse
            keyByteArray = keyByteArray.Select(b => (byte)((b * 0x0202020202 & 0x010884422010) % 1023)).ToArray();
            logger.Log($"Reversed key (7 bytes): {BitConverter.ToString(keyByteArray)}");

            var keyBitArray = new BitArray(keyByteArray);
            logger.Log($"As bits: {keyBitArray.ToBitString()}");

            var resultBitArray = new BitArray(64);
            int counter = 0;
            for (int i = 0, j = 0; i < keyBitArray.Length + 1; i++, j++)
            {
                if ((j + 1) % 8 == 0 && i != 0)
                {
                    resultBitArray[j] = counter % 2 == 0;
                    if (i == keyBitArray.Length)
                    {
                        break;
                    }
                    counter = keyBitArray[i] ? 1 : 0;
                    j++;
                    resultBitArray[j] = keyBitArray[i];
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
            logger.Log($"With parity bits: {BitConverter.ToString(result)}");
            logger.Log($"As bits: {resultBitArray.ToBitString()}");

            // Reverse
            result = result.Select(b => (byte)((b * 0x0202020202 & 0x010884422010) % 1023)).ToArray();
            logger.Log($"And reversed: {BitConverter.ToString(result)}");
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