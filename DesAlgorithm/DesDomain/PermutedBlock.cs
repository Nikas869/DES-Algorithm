using System.Collections;

namespace DesAlgorithm.DesDomain
{
    public class PermutedBlock
    {
        public PermutedBlock(BitArray left, BitArray right)
        {
            this.Left = left;
            this.Right = right;
        }

        public BitArray Left { get; }

        public BitArray Right { get; }
    }
}
