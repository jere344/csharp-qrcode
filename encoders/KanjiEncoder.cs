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
        for (int i = 0; i < bytes.Length; i++)
        {
            if (bytes[i] >= 0x8140 && bytes[i] <= 0x9FFC)
            {
                // subtract 0x8140
                var subtracted = bytes[i] - 0x8140;
                // split into most and least significant bytes
                var mostSignificantByte = subtracted >> 8;
                var leastSignificantByte = subtracted & 0xFF;
                // multiply most significant byte by 0xC0, then add least significant byte
                var multiplied = mostSignificantByte * 0xC0 + leastSignificantByte;
                // convert to 13-bit binary
                var binary = Convert.ToString(multiplied, 2).PadLeft(13, '0');
                encoded[i] = binary;
            }
            else if (bytes[i] >= 0xE040 && bytes[i] <= 0xEBBF)
            {
                // subtract 0xC140
                var subtracted = bytes[i] - 0xC140;
                // split into most and least significant bytes
                var mostSignificantByte = subtracted >> 8;
                var leastSignificantByte = subtracted & 0xFF;
                // multiply most significant byte by 0xC0, then add least significant byte
                var multiplied = mostSignificantByte * 0xC0 + leastSignificantByte;
                // convert to 13-bit binary
                var binary = Convert.ToString(multiplied, 2).PadLeft(13, '0');
                encoded[i] = binary;
            }
        }

        return encoded;

    }



}
