using QRGenerator.encoders;

namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = "";
            //for (int i = 0; i < 2953; i++)
            //{
            //    text += "0";
            //}

            var qr = new QRCodeGenerator("sdf", ErrorCorrectionLevels.Q);
            Console.WriteLine(qr.Encoder.EncodingMode);
            Console.WriteLine(qr.Encoder.Version);

            Console.WriteLine(qr.Encoder.Encode());

            
        }
    }
}