﻿using System.Collections;
using System.Text;

namespace DesAlgorithm
{
    public static class Extensions
    {
        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }

        public static BitArray GetRange(this BitArray bits, int startIndex, int length)
        {
            var result = new BitArray(length);

            for (int i = startIndex; i < startIndex + length; i++)
            {
                result[i] = bits[i];
            }

            return result;
        }

        public static BitArray CycleLeftShift(this BitArray bits, int shift)
        {
            var result = new BitArray(bits.Length);

            for (int originalIdx = 0, shiftedIdx = shift; originalIdx < bits.Length; originalIdx++)
            {
                shiftedIdx = shiftedIdx == bits.Length ? 0 : shiftedIdx + 1;
                result[originalIdx] = bits[shiftedIdx];
            }

            return result;
        }

        public static BitArray Concat(this BitArray first, BitArray second)
        {
            bool[] combined = new bool[first.Length + second.Length];

            first.CopyTo(combined, 0);
            second.CopyTo(combined, first.Length);

            return new BitArray(combined);
        }
    }
}