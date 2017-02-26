using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace DesAlgorithm
{
    public static class Des
    {
        public static string GenerateKey()
        {
            byte[] randomBytes = GetRandomBytes(8);
            BitArray keyBitArray = new BitArray(randomBytes);
            SetParityBits(keyBitArray);
            byte[] resultBytes = new byte[(keyBitArray.Length - 1) / 8 + 1];
            keyBitArray.CopyTo(resultBytes, 0);

            return Encoding.ASCII.GetString(resultBytes);
        }

        private static void SetParityBits(BitArray keyBitArray)
        {
            int counter = 0;
            for (int i = 0; i < keyBitArray.Length; i++)
            {
                if ((i + 1) % 8 == 0 && i != 0)
                {
                    keyBitArray[i] = counter % 2 == 0;
                    counter = 0;
                }
                else if (keyBitArray[i])
                {
                    counter++;
                }
            }
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