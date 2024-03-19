using QRGenerator.encoders;
using QRGenerator.ImageGenerator;
using SkiaSharp;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Entrez la chaine de caractères à encoder, ou laissez vide pour le test par défaut (les version 6 et suppérieures ainsi que les kanji ne sont pas supportés) : ");
            // string? text = "" ;// Console.ReadLine();
            // if (text == "" || text == null)
            // {
            //     text = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
            // }
            // var qr = new QRCodeGenerator(text, ErrorCorrectionLevels.H, version: 20);

            // string logoPath = "C:\\Users\\2230460\\Desktop\\logo2.png";

            // qr.ExportImage(scale: 40, patternColor: SKColors.Green, logoPath: logoPath, logoShadowType: "circle");

            // Console.WriteLine("Le fichier output.png à été sauvegardé dans le dossier courant");

            // DisplayMatrix(qr.Matrix);

            string color = "#800040";
            SKColor? patternColor = color is null ? null : SKColor.Parse(color);
            System.Console.WriteLine(patternColor);
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