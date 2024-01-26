using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.encoders
{
    public class ByteEncoder
    {
        public string[] ByteEncode(string text)
        {
            // check if UTF-8 is needed
            // if so, encode as UTF-8
            // else, encode as ISO-8859-1

            for (int i = 0; i < text.Length; i++)
            {
                // char will be converted to its ASCII value
                if (text[i] > 255) // if any character is outside of the ISO-8859-1 range
                {
                    return UTF8Encode(text);
                }
            }
            return ISO88591Encode(text);

            return UTF8Encode(text);

        }

        public string[] UTF8Encode(string text)
        {
            string UTF8EncodedText = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(text));

            // Step 1: Split the string into 8-bit bytes.
            string[] bytes = new string[UTF8EncodedText.Length];
            for (int i = 0; i < UTF8EncodedText.Length; i++)
            {
                bytes[i] = Convert.ToString(UTF8EncodedText[i], 2).PadLeft(8, '0');
            }

            // Step 2: Split the bytes into groups of 8.
            string[] groups = new string[bytes.Length / 8 + 1];
            int groupIndex = 0;
            for (int i = 0; i < bytes.Length; i += 8)
            {
                if (i + 8 <= bytes.Length)
                {
                    groups[groupIndex] = bytes[i] + bytes[i + 1] + bytes[i + 2] + bytes[i + 3] + bytes[i + 4] + bytes[i + 5] + bytes[i + 6] + bytes[i + 7];
                }
                else
                {
                    groups[groupIndex] = bytes[i];
                }
                groupIndex++;
            }

            return groups;



        }

        public string[] ISO88591Encode(string text)
        {
            string ISO88591EncodedText = Encoding.GetEncoding("ISO-8859-1").GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(text));

            // Step 1: Split the string into 8-bit bytes.
            string[] bytes = new string[ISO88591EncodedText.Length];
            for (int i = 0; i < ISO88591EncodedText.Length; i++)
            {
                bytes[i] = Convert.ToString(ISO88591EncodedText[i], 2).PadLeft(8, '0');
            }

            // Step 2: Split the bytes into groups of 8.
            string[] groups = new string[bytes.Length / 8 + 1];
            int groupIndex = 0;
            for (int i = 0; i < bytes.Length; i += 8)
            {
                if (i + 8 <= bytes.Length)
                {
                    groups[groupIndex] = bytes[i] + bytes[i + 1] + bytes[i + 2] + bytes[i + 3] + bytes[i + 4] + bytes[i + 5] + bytes[i + 6] + bytes[i + 7];
                }
                else
                {
                    groups[groupIndex] = bytes[i];
                }
                groupIndex++;
            }

            return groups;
        }


    }
}