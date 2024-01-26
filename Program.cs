namespace QRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var numericEncoder = new NumericEncoder();
            var groups = numericEncoder.NumericEncode("86753009");
            foreach (var group in groups)
            {
                Console.WriteLine(group);
            }
        }
    }
}