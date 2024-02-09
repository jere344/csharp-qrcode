using QRGenerator.encoders;
using QRGenerator.ImageGenerator;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var qr = new QRCodeGenerator("HELLO WORLD", ErrorCorrectionLevels.Q, 5, SupportedEncodingMode.Alphanumeric);
            //Console.WriteLine(qr.EncodingMode);
            //Console.WriteLine(qr.Version);
            //Console.WriteLine(qr.EncodedText);

            //int[,] table = new int[0, 0];

            bool?[,] matrix = new MatrixGenerator(21).Matrix;

            matrix = QrMetadataPlacer.addTimingPatern(matrix);
            matrix = QrMetadataPlacer.addAllFinderPaterns(matrix);
            matrix = QrMetadataPlacer.addDarkModule(matrix);
            matrix = QrMetadataPlacer.addSeparators(matrix);


            int[] data = new int[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9,
            };
            var fillData = new QrDataFiller(matrix, data);
            int counter = 0;
            foreach (var (y, x) in fillData.GetNextPosition())
            {
                matrix[y, x] = true;
                counter++;
                Console.WriteLine("Matrix " + counter);
                DisplayMatrix(matrix);
            }
        }

        static void DisplayMatrix(bool?[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == null)
                    {
                        Console.Write("- ");
                    }
                    else if (matrix[i, j] == false)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write("o ");
                    }

                }
                Console.WriteLine();
            }
        }
    }
}