using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.MatrixGenerator
{
    internal class MatrixGenerator
    {
        public int Size { get; set; }
        public int[,] Matrix { get; set; }
        // public QrMetadataPlacer QrMetadataPlacer { get; set; }
        public MatrixGenerator(int size)
        {
            // 1 = white
            // 0 = black
            // null = not set
            this.Size = size;
            this.Matrix = new int[size, size];
        }
    }
}
