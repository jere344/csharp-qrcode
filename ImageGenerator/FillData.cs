using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.ImageGenerator
{
    internal class FillData
    {
        public int[,] Matrix { get; set; }
        public int[] Data { get; set; }
        public FillData(int[,] ints, int[] data)
        {
            Matrix = ints;
            Data = data;
        
        }

        /// <summary>
        /// Generator (yield) that gives a list of the next position to fill in the matrix
        /// Exemple of the return : [[21, 21], [20, 21], [21, 20], [20, 20], [21, 19], [20, 19], [21, 18], [20, 18]]
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(int, int)> GetNextPosition()
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

                if (Matrix[y, x] == 2)
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
                    if (y - direction < 0 || y - direction >= size || Matrix[y - direction, x] != 2)
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
    }
}
