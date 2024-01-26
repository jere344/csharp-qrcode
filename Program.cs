using QRGenerator.encoders;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var numericEncoder = new AlphanumericEncoder();
            var encoded = numericEncoder.AlphanumericEncode("HELLO WORLD");
            foreach (var item in encoded)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}