using QRGenerator.encoders;
using QRGenerator.ImageGenerator;
using SkiaSharp;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entrez la chaine de caractères à encoder, ou laissez vide pour le test par défaut : ");
            string? text = Console.ReadLine();
            if (text == "" || text == null)
            {
                text = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
            }
            var qr = new QRCodeGenerator(text);

            qr.ExportImage(scale: 10);

            Console.WriteLine("Le fichier à été sauvegardé dans le dossier courant");

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