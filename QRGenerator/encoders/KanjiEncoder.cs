using System.Threading.Tasks;
using System.Text;

namespace QRGenerator.encoders;

public static class KanjiEncoder
{
    // Not implemented yet, shift_jis encoding is not supported natively
    public static string[] KanjiEncode(string text)
    {
        // Step 1: Convert to Bytes
        byte[] bytes = Encoding.GetEncoding("shift_jis").GetBytes(text);

        // Step 2 : Encode the Bytes with Kanji Mode
        var encoded = new string[bytes.Length];

        return encoded;
    }
}