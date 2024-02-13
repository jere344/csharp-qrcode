using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.ImageGenerator
{
    internal static class QrDataFiller
    {

        /// <summary>
        /// Convert the data to bits. Each data number is a byte. We convert each byte to 8 bits
        /// </summary>
        /// <returns></returns>
        private static bool[] GetBits(List<int> Data)
        {
            var bits = new bool[Data.Count * 8];
            for (int i = 0; i < Data.Count; i++)
            {
                var byteBits = Convert.ToString(Data[i], 2).PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    bits[i * 8 + j] = byteBits[j] == '1';
                }
            }
            return bits;
        }
        
        /// <summary>
        /// Generator (yield) that gives a list of the next position to fill in the matrix
        /// Exemples of the return : [21, 21], [20, 21], [21, 20], [20, 20], [21, 19], [20, 19], [21, 18], [20, 18]
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<(int, int)> GetNextPosition(bool?[,] Matrix)
        {
            // The data bits are placed starting at the bottom - right of the matrix and proceeding upward in a column that is 2 modules wide. Use white pixels for 0, and black pixels for 1.When the column reaches the top, the next 2 - module column starts immediately to the left of the previous column and continues downward.Whenever the current column reaches the edge of the matrix, move on to the next 2 - module column and change direction.If a function pattern or reserved area is encountered, the data bit is placed in the next unused module.
            int size = Matrix.GetLength(0);
            int x = size - 1;
            int y = size - 1;
            int direction = 1; // 1 = up, -1 = down
            int counter = 0;
            while (x > -1) 
            {
                // We always skip the timing pattern at column 6
                if (x == 6) { x -= 1; }
                // We always skip the timing pattern at row 6
                if (y == 6) { y -= direction; }

                // if the cell is not empty we just go next and don't yield it
                if (Matrix[y, x] == null)
                {
                    yield return (y, x);
                }
                if (counter % 2 == 0)
                {
                    x -= 1;
                }
                else
                {     
                     x += 1;
                    // if we reach the edge of the matrix || we reach a filled cell
                    if (y - direction < 0 || y - direction >= size)
                    {
                        // change direction
                        direction *= -1;
                        // move to the next double column module
                        x -= 2;
                    }
                    else
                    {
                        // else we just move up or down
                        y -= direction;
                    }

                }

                counter++;
            }
        }
    
        /// <summary>
        /// Fill the matrix with the data, avoiding the reserved areas in metadataMatrix
        /// </summary>
        /// <returns></returns>
        public static bool?[,] FillMatrix(bool?[,] dataMatrix, bool?[,] metadataMatrix, List<int> data)
        {
            var bits = GetBits(data);
            var counter = 0;
            foreach (var (y, x) in GetNextPosition(metadataMatrix))
            {
                dataMatrix[y, x] = bits[counter];
                counter++;
            }
            return dataMatrix;
        }
    }
    
}
