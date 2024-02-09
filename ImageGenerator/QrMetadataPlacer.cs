using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.ImageGenerator
{
    internal class QrMetadataPlacer
    {

        public static bool isEven(int nb)
        {
            if (nb % 2 == 0)
            { return true; }
            else { return false; }
        }

        public static bool?[,] addTimingPatern(bool?[,] table)
        {


            // timing patern horizontal
            for (int i = 8; i <= table.GetLength(0) - 9; i++)
            {
                if (isEven(i))
                {
                    table[6, i] = false;
                }
                else
                {
                    table[6, i] = true;
                }
            }

            // timing patern vertical
            for (int i = 8; i <= table.GetLength(0) - 9; i++)
            {
                if (isEven(i))
                {
                    table[i, 6] = false;
                }
                else
                {
                    table[i, 6] = true;
                }
            }
            return table;

        }

        /// <summary>
        /// créée les 3 finder paterns
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool?[,] addFinderPatern(int x, int y, bool?[,] table)
        {

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    table[x + i, y + j] = ((i == 0 || i == 7 - 1) || (j == 0 || j == 7 - 1)) ? false : true;
                }

            }

            for (int i = 2; i < 7 - 2; i++)
            {
                for (int j = 2; j < 7 - 2; j++)
                {
                    table[x + i, y + j] = false;
                }
            }

            return table;
        }

        public static bool?[,] addAllFinderPaterns(bool?[,] table)
        {
            //topleft
            table = addFinderPatern(0, 0, table);

            //topright
            table = addFinderPatern(0, table.GetLength(1) - 7, table);

            //botleft
            table = addFinderPatern(table.GetLength(1) - 7, 0, table);

            return table;

        }



        public static bool?[,] addSeparators(bool?[,] table)
        {
            // top right
            int x = table.GetLength(1) - 8;
            int y = 7;

            for (int j = y; j >= 0; j--)
            {
                table[j, x] = true;
            }
            for (int i = x; i < table.GetLength(1); i++)
            {
                table[y, i] = true;
            }

            // bottom left
            x = 7;
            y = table.GetLength(0) - 8;

            for (int i = x; i >= 0; i--)
            {
                table[y, i] = true;
            }
            for (int j = y; j < table.GetLength(0); j++)
            {
                table[j, x] = true;
            }

            // top left
            x = 7;
            y = 7;

            for (int i = x; i >= 0; i--)
            {
                table[y, i] = true;
            }
            for (int j = y; j >= 0; j--)
            {
                table[j, x] = true;
            }


            return table;

        }

        public static bool?[,] addDarkModule(bool?[,] table)
        {
            int x = 8;
            int y = table.GetLength(1) - 8;
            table[y, x] = false;
            return table;

        }

    }
}
