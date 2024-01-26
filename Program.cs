using QRGenerator.encoders;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var numericEncoder = new KanjiEncoder();
            var encoded = numericEncoder.KanjiEncode("茗荷 ");
            foreach (var item in encoded)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}