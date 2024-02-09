using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.ImageGenerator;

internal class MatrixGenerator
{
    public int Size { get; set; }
    public bool?[,] Matrix { get; set; }
    // public QrMetadataPlacer QrMetadataPlacer { get; set; }
    public MatrixGenerator(int size)
    {
        // 0 = white
        // 1 = black
        // 2 = not set
        this.Size = size;
        this.Matrix = new bool?[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                this.Matrix[i, j] = null;
            }
        }
    }
}
