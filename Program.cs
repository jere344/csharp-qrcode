using QRGenerator.encoders;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var qr = new QRCodeGenerator("HELLO WORLD", ErrorCorrectionLevels.L, 1, SupportedEncodingMode.Alphanumeric);
            Console.WriteLine(qr.EncodingMode);
            Console.WriteLine(qr.Version);
            Console.WriteLine(qr.EncodedText);

            int[,] table = new int[0, 0];
            List<List<int>> table = new List<List<int>>();

            
        }
    }
}