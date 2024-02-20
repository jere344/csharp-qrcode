using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.ImageGenerator
{
    internal static class QrMetadataPlacer
    {
        /// <summary>
        /// Add the format information to the QR code
        /// </summary>
        /// <param name="Matrix"></param>
        /// <param name="formatString"></param>
        /// <returns> The QR code with the format information</returns>
        public static bool?[,] AddFormatInformation(bool?[,] Matrix, bool[] formatString)
        {
            int x = 0;
            int y = 8;
            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                if (i == 6) { continue; }
                Matrix[y, x + i] = formatString[counter];
                counter++;
            }
            x = 8;
            y = 7;
            for (int i = 0; i < 8; i++)
            {
                if (i == 1) { continue; }
                Matrix[y - i, x] = formatString[counter];
                counter++;
            }

            counter = 0;
            x = 8;
            y = Matrix.GetLength(0) - 1;
            for (int i = 0; i < 7; i++)
            {
                Matrix[y - i, x] = formatString[counter];
                counter++;
            }
            x = Matrix.GetLength(1) - 8;
            y = 8;
            for (int i = 0; i < 8; i++)
            {
                Matrix[y, x + i] = formatString[counter];
                counter++;
            }

            return Matrix;
        }

        /// <summary>
        /// Add the version information to the QR code
        /// </summary>
        /// <param name="Matrix"></param>
        /// <param name="versionString"></param>
        /// <returns> The QR code with the version information</returns>
        public static bool?[,] AddVersionInformation(bool?[,] Matrix, bool[] versionString)
        {
            int counter = 0;
            int x = Matrix.GetLength(1) - 9;
            int y = 5;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Matrix[y - i, x - j] = versionString[counter];
                    counter++;
                }
            }

            counter = 0;
            x = 5;
            y = Matrix.GetLength(0) - 9;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Matrix[y - j, x - i] = versionString[counter];
                    counter++;
                }
            }

            return Matrix;
        }

        /// <summary>
        /// Add all the metadata to the QR code
        /// </summary>
        /// <param name="table"></param>
        /// <param name="Version"></param>
        /// <returns> The QR code with all the metadata</returns>
        public static bool?[,] AddAllMetadata(bool?[,] table, int Version)
        {
            table = AddAllFinderPaterns(table);
            table = AddDarkModule(table);
            table = AddSeparators(table);
            table = AddFormatInfoArea(table, Version);
            table = AddAlignmentPatterns(table, Version);
            table = AddTimingPatern(table);
            return table;
        }

        private static bool IsEven(int nb)
        {
            if (nb % 2 == 0)
            { return true; }
            else { return false; }
        }

        /// <summary>
        /// Add the timing patern to the QR code
        /// </summary>
        /// <param name="table"></param>
        /// <returns> The QR code with the timing patern</returns>
        private static bool?[,] AddTimingPatern(bool?[,] table)
        {
            // timing patern horizontal
            for (int i = 8; i <= table.GetLength(0) - 9; i++)
            {
                if (IsEven(i))
                {
                    table[6, i] = true;
                }
                else
                {
                    table[6, i] = false;
                }
            }

            // timing patern vertical
            for (int i = 8; i <= table.GetLength(0) - 9; i++)
            {
                if (IsEven(i))
                {
                    table[i, 6] = true;
                }
                else
                {
                    table[i, 6] = false;
                }
            }
            return table;
        }

        /// <summary>
        /// créée le finder patern à la position x, y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="table"></param>
        /// <returns> The QR code with the finder patern</returns>
        private static bool?[,] AddFinderPatern(int x, int y, bool?[,] table)
        {

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    table[x + i, y + j] = ((i == 0 || i == 7 - 1) || (j == 0 || j == 7 - 1));
                }

            }

            for (int i = 2; i < 7 - 2; i++)
            {
                for (int j = 2; j < 7 - 2; j++)
                {
                    table[x + i, y + j] = true;
                }
            }

            return table;
        }

        /// <summary>
        /// Add the 3 finder paterns to the QR code
        /// </summary>
        /// <param name="table"></param>
        /// <returns> The QR code with the 3 finder paterns</returns>
        private static bool?[,] AddAllFinderPaterns(bool?[,] table)
        {
            //topleft
            table = AddFinderPatern(0, 0, table);

            //topright
            table = AddFinderPatern(0, table.GetLength(1) - 7, table);

            //botleft
            table = AddFinderPatern(table.GetLength(1) - 7, 0, table);

            return table;

        }

        /// <summary>
        /// Add the format information area to the QR code
        /// </summary>
        /// <param name="table"></param>
        /// <param name="version"></param>
        /// <returns> The QR code with the format information area</returns>
        private static bool?[,] AddFormatInfoArea(bool?[,] table, int version)
        {
            // top right
            int x = table.GetLength(1) - 8;
            int y = 8;

            for (int i = x; i < table.GetLength(1); i++)
            {
                table[y, i] = false;
            }

            // bottom left
            x = 8;
            y = table.GetLength(0) - 7;

            for (int j = y; j < table.GetLength(0); j++)
            {
                table[j, x] = false;
            }

            // top left
            x = 8;
            y = 8;

            for (int i = x; i >= 0; i--)
            {
                if (table[y, i] == null) { table[y, i] = false; }
            }
            for (int j = y; j >= 0; j--)
            {
                if (table[j, x] == null) { table[j, x] = false; }
            }


            if (version >= 7)
            {

                for (int i = 0; i <= 5; i++)
                {
                    for (int j = table.GetLength(0) - 11; j <= table.GetLength(0) - 9; j++)
                    {
                        table[i, j] = false;
                        table[j, i] = false;
                    }
                }

            }

            return table;
        }

        /// <summary>
        /// Add the separators to the QR code
        /// </summary>
        /// <param name="table"></param>
        /// <returns> The QR code with the separators</returns>
        private static bool?[,] AddSeparators(bool?[,] table)
        {
            // top right
            int x = table.GetLength(1) - 8;
            int y = 7;

            for (int j = y; j >= 0; j--)
            {
                table[j, x] = false;
            }
            for (int i = x; i < table.GetLength(1); i++)
            {
                table[y, i] = false;
            }

            // bottom left
            x = 7;
            y = table.GetLength(0) - 8;

            for (int i = x; i >= 0; i--)
            {
                table[y, i] = false;
            }
            for (int j = y; j < table.GetLength(0); j++)
            {
                table[j, x] = false;
            }

            // top left
            x = 7;
            y = 7;

            for (int i = x; i >= 0; i--)
            {
                table[y, i] = false;
            }
            for (int j = y; j >= 0; j--)
            {
                table[j, x] = false;
            }


            return table;

        }

        /// <summary>
        /// Add the dark module to the QR code
        /// </summary>
        /// <param name="table"></param>
        /// <returns> The QR code with the dark module</returns>
        private static bool?[,] AddDarkModule(bool?[,] table)
        {
            int x = 8;
            int y = table.GetLength(1) - 8;
            table[y, x] = true;
            return table;

        }

        /// <summary>
        /// Get the position of the alignment patterns
        /// </summary>
        /// <param name="version"></param>
        /// <returns> The position of the alignment patterns</returns>
        private static int[] GetAlignmentPatternPositions(int version)
        {
            int[] positions;

            switch (version)
            {
                case 2:
                    positions = new int[] { 6, 18 };
                    break;
                case 3:
                    positions = new int[] { 6, 22 };
                    break;
                case 4:
                    positions = new int[] { 6, 26 };
                    break;
                case 5:
                    positions = new int[] { 6, 30 };
                    break;
                case 6:
                    positions = new int[] { 6, 34 };
                    break;
                case 7:
                    positions = new int[] { 6, 22, 38 };
                    break;
                case 8:
                    positions = new int[] { 6, 24, 42 };
                    break;
                case 9:
                    positions = new int[] { 6, 26, 46 };
                    break;
                case 10:
                    positions = new int[] { 6, 28, 50 };
                    break;
                case 11:
                    positions = new int[] { 6, 30, 54 };
                    break;
                case 12:
                    positions = new int[] { 6, 32, 58 };
                    break;
                case 13:
                    positions = new int[] { 6, 34, 62 };
                    break;
                case 14:
                    positions = new int[] { 6, 26, 46, 66 };
                    break;
                case 15:
                    positions = new int[] { 6, 26, 48, 70 };
                    break;
                case 16:
                    positions = new int[] { 6, 26, 50, 74 };
                    break;
                case 17:
                    positions = new int[] { 6, 30, 54, 78 };
                    break;
                case 18:
                    positions = new int[] { 6, 30, 56, 82 };
                    break;
                case 19:
                    positions = new int[] { 6, 30, 58, 86 };
                    break;
                case 20:
                    positions = new int[] { 6, 34, 62, 90 };
                    break;
                case 21:
                    positions = new int[] { 6, 28, 50, 72, 94 };
                    break;
                case 22:
                    positions = new int[] { 6, 26, 50, 74, 98 };
                    break;
                case 23:
                    positions = new int[] { 6, 30, 54, 78, 102 };
                    break;
                case 24:
                    positions = new int[] { 6, 28, 54, 80, 106 };
                    break;
                case 25:
                    positions = new int[] { 6, 32, 58, 84, 110 };
                    break;
                case 26:
                    positions = new int[] { 6, 30, 58, 86, 114 };
                    break;
                case 27:
                    positions = new int[] { 6, 34, 62, 90, 118 };
                    break;
                case 28:
                    positions = new int[] { 6, 26, 50, 74, 98, 122 };
                    break;
                case 29:
                    positions = new int[] { 6, 30, 54, 78, 102, 126 };
                    break;
                case 30:
                    positions = new int[] { 6, 26, 52, 78, 104, 130 };
                    break;
                case 31:
                    positions = new int[] { 6, 30, 56, 82, 108, 134 };
                    break;
                case 32:
                    positions = new int[] { 6, 34, 60, 86, 112, 138 };
                    break;
                case 33:
                    positions = new int[] { 6, 30, 58, 86, 114, 142 };
                    break;
                case 34:
                    positions = new int[] { 6, 34, 62, 90, 118, 146 };
                    break;
                case 35:
                    positions = new int[] { 6, 30, 54, 78, 102, 126, 150 };
                    break;
                case 36:
                    positions = new int[] { 6, 24, 50, 76, 102, 128, 154 };
                    break;
                case 37:
                    positions = new int[] { 6, 28, 54, 80, 106, 132, 158 };
                    break;
                case 38:
                    positions = new int[] { 6, 32, 58, 84, 110, 136, 162 };
                    break;
                case 39:
                    positions = new int[] { 6, 26, 54, 82, 110, 138, 166 };
                    break;
                case 40:
                    positions = new int[] { 6, 30, 58, 86, 114, 142, 170 };
                    break;

                default:
                    positions = new int[] { };
                    break;

            }
            return positions;
        }

        /// <summary>
        /// Add the alignment patterns to the QR code
        /// </summary>
        /// <param name="table"></param>
        /// <param name="version"></param>
        /// <returns> The QR code with the alignment patterns</returns>
        private static bool?[,] AddAlignmentPatterns(bool?[,] table, int version)
        {
            int[] positions = GetAlignmentPatternPositions(version);

            foreach (int x in positions)
            {
                foreach (int y in positions)
                {
                    if (table[x, y] != null) { continue; }

                    // paint 4x4 in black
                    for (int i = -2; i <= 2; i++)
                    {
                        for (int j = -2; j <= 2; j++)
                        {
                            table[x + i, y + j] = true;
                        }
                    }

                    // then 3x3 in white
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            table[x + i, y + j] = false;
                        }
                    }

                    // then the center in black
                    table[x, y] = true;
                }
            }
            return table;
        }
    }
}
