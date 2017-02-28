using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;

namespace DesAlgorithm
{
    public class Des
    {
        private static readonly ArrayList WeakKeys = new ArrayList
        {
            new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 },
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE },
            new byte[] { 0x1F, 0x1F, 0x1F, 0x1F, 0x0E, 0x0E, 0x0E, 0x0E },
            new byte[] { 0xE0, 0xE0, 0xE0, 0xE0, 0xF1, 0xF1, 0xF1, 0xF1 }
        };

        private static readonly int[] PermutedKey1 = 
        {
            57, 49, 41, 33, 25, 17, 9,
            1,  58, 50, 42, 34, 26, 18,
            10, 2,  59, 51, 43, 35, 27,
            19, 11, 3,  60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7,  62, 54, 46, 38, 30, 22,
            14, 6,  61, 53, 45, 37, 29,
            21, 13, 5,  28, 20, 12, 4 
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
            } while (WeakKeys.Contains(keyByteArray)); 

            return BitConverter.ToString(keyByteArray).Replace("-", string.Empty);
        }

        public byte[] SetParityBits(byte[] keyByteArray)
        {
            logger.Log($"Generated key (7 bytes): {BitConverter.ToString(keyByteArray)}");

            // Reverse
            keyByteArray = keyByteArray.ReverseBytes();
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
            result = result.ReverseBytes();
            logger.Log($"And reversed: {BitConverter.ToString(result)}");
            return result;
        }

        private byte[] GetRandomBytes(int length)
        {
            byte[] result = new byte[length];
            RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();

            randomNumberGenerator.GetBytes(result);

            return result;
        }

        public byte[] GetPermutedKey(byte[] originalKey)
        {
            originalKey = originalKey.ReverseBytes();

            var originalKeyBits = new BitArray(originalKey);
            var permutedKeyBits = new BitArray(56);

            for (int i = 0; i < PermutedKey1.Length; i++)
            {
                permutedKeyBits[i] = originalKeyBits[PermutedKey1[i] - 1];
            }

            var permutedKey = new byte[7];
            permutedKeyBits.CopyTo(permutedKey, 0);

            permutedKey = permutedKey.ReverseBytes();

            return permutedKey;
        }
    }
}