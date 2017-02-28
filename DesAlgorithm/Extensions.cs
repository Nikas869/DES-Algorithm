using System.Collections;
using System.Linq;
using System.Text;

namespace DesAlgorithm
{
    public static class Extensions
    {
        // Creates string from BitArray (example 0111 0010 => "01110010")
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

        // Magic
        public static byte[] ReverseBytes(this byte[] bytes)
        {
            return bytes.Select(b => (byte)((b * 0x0202020202 & 0x010884422010) % 1023)).ToArray();
        }
    }
}