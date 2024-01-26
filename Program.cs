using QRGenerator.encoders;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var qr = new QRCodeGenerator("Hello, World!");
            Console.WriteLine(qr.Encoder.EncodingMode);
            foreach(var s in qr.Encoder.Encode(qr.TextToEncode))
            {
                System.Console.WriteLine(s);
            }
        }
    }
}