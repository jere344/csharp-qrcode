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
            bool warning_triggered = false;
            foreach (var (y, x) in GetNextPosition(metadataMatrix))
            {
                if (warning_triggered == true)
                {
                    dataMatrix[y, x] = false;
                    continue;
                }

                if (counter >= bits.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WARNING: Data lenght do not correspond to the matrix size. Fill the rest of the matrix with white pixels");
                    Console.ResetColor();
                    warning_triggered = true;
                    dataMatrix[y, x] = false;
                }
                else
                {
                    dataMatrix[y, x] = bits[counter];
                    counter++;
                }
            }
            return dataMatrix;
        }
    }

}
