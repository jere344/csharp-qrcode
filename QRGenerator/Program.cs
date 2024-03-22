using QRGenerator.encoders;
using QRGenerator.ImageGenerator;
using SkiaSharp;

namespace QRGenerator
{
    class Program
    {
        // static void Main(string[] args)
        // {
        //     Console.WriteLine("Entrez la chaine de caractères à encoder, ou laissez vide pour le test par défaut : ");
        //     string? text = Console.ReadLine();
        //     if (text == "" || text == null)
        //     {
        //         text = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        //     }
        //     var qr = new QRCodeGenerator(text);

        //     qr.ExportImage(scale: 10);

        //     Console.WriteLine("Le fichier à été sauvegardé dans le dossier courant");

        //     DisplayMatrix(qr.Matrix);
        // }

        // int scale = 50, string path = "qrcode.png", SKColor? patternColor = null, string? logoPath = null, string logoShadowType = "circle", SKColor? backgroundColor = null, string text, ErrorCorrectionLevels errorCorrectionLevel = ErrorCorrectionLevels.L, int? version = null, SupportedEncodingMode? encodingMode = null, int? mask = null

        static void Main(string[] args)
        {
            string text = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
            int scale = 10;
            string path = "qrcode.png";
            SKColor? patternColor = null;
            string? logoPath = null;
            string logoShadowType = "circle";
            SKColor? backgroundColor = null;
            ErrorCorrectionLevels errorCorrectionLevel = ErrorCorrectionLevels.L;
            int? version = null;
            SupportedEncodingMode? encodingMode = null;
            int? mask = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-t":
                        text = args[i + 1];
                        break;
                    case "-s":
                        scale = int.Parse(args[i + 1]);
                        break;
                    case "-p":
                        path = args[i + 1];
                        break;
                    case "-pc":
                        patternColor = SKColor.Parse(args[i + 1]);
                        break;
                    case "-l":
                        logoPath = args[i + 1];
                        break;
                    case "-ls":
                        logoShadowType = args[i + 1];
                        break;
                    case "-bc":
                        backgroundColor = SKColor.Parse(args[i + 1]);
                        break;
                    case "-e":
                        errorCorrectionLevel = (ErrorCorrectionLevels)Enum.Parse(typeof(ErrorCorrectionLevels), args[i + 1]);
                        break;
                    case "-v":
                        version = int.Parse(args[i + 1]);
                        break;
                    case "-em":
                        encodingMode = (SupportedEncodingMode)Enum.Parse(typeof(SupportedEncodingMode), args[i + 1]);
                        break;
                    case "-m":
                        mask = int.Parse(args[i + 1]);
                        break;
                    case "-h":
                    case "--help":
                    
                        Console.WriteLine("Usage: QRGenerator [-t text] [-s scale] [-p path] [-pc patternColor] [-l logoPath] [-ls logoShadowType] [-bc backgroundColor] [-e errorCorrectionLevel] [-v version] [-em encodingMode] [-m mask] [-h]");
                        Console.WriteLine("Options:");
                        Console.WriteLine("  -t text: Text to encode");
                        Console.WriteLine("  -s scale: Scale of the QRCode");
                        Console.WriteLine("  -p path: Path to save the QRCode");
                        Console.WriteLine("  -pc patternColor: HTML color code of the pattern");
                        Console.WriteLine("  -l logoPath: Path to the logo");
                        Console.WriteLine("  -ls logoShadowType: Type of shadow for the logo (circle, shadow, cicle+shadow)");
                        Console.WriteLine("  -bc backgroundColor: HTML color code for the Background");
                        Console.WriteLine("  -e errorCorrectionLevel: Error correction level (L, M, Q, H)");
                        Console.WriteLine("  -v version: Version of the QRCode");
                        Console.WriteLine("  -em encodingMode: Encoding mode (Numeric, Alphanumeric, Byte)");
                        Console.WriteLine("  -m mask: Mask to apply");
                        Console.WriteLine("  -h: Display this help message");
                        return;
                    case "--version":
                        Console.WriteLine("QRGenerator v2.0.1");
                        return;
                }
            }

            var qr = new QRCodeGenerator(text, errorCorrectionLevel, version, encodingMode, mask);

            qr.ExportImage(scale, path, patternColor, logoPath, logoShadowType, backgroundColor);

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