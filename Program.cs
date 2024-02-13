using QRGenerator.encoders;
using QRGenerator.ImageGenerator;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var qr = new QRCodeGenerator("HELLO WORLD");
            Console.WriteLine(qr.EncodingMode);
            Console.WriteLine(qr.Version);
            Console.WriteLine(qr.EncodedText);

            Console.WriteLine(qr.FormatString);
            DisplayMatrix(qr.Matrix);
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