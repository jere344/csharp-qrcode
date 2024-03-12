using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.encoders
{
    public static class ByteEncoder
    {

        /// <summary>
        /// Encode the text into binary numbers
        /// </summary>
        /// <param name="text"></param>
        /// <returns> A string of binary numbers</returns>
        public static string ByteEncode(string text)
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
        }

        /// <summary>
        /// Encode a text using the UTF-8 character set into binary numbers
        /// </summary>
        /// <param name="text"></param>
        /// <returns> A string of binary numbers</returns>
        public static string UTF8Encode(string text)
        {

            // Step 1: Split the string into 8-bit bytes.

            // After converting your input string into ISO 8859 - 1, or UTF-8 if your users have QR code readers that can recognize it in byte mode, you must split the string into 8 - bit bytes.
            // For example, we will use the input string "Hello, world!" to create a version 1 QR code. Since it contains lowercase letters, a comma, and an exclamation mark, it can't be encoded with alphanumeric mode, which does not include lowercase letters, commas, or exclamation marks. 

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            string[] byteStrings = new string[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                byteStrings[i] = Convert.ToString(bytes[i], 2).PadLeft(8, '0');
            }
            return string.Join("", byteStrings);

        }

        /// <summary>
        /// Encode a text using the ISO-8859-1 character set into binary numbers
        /// </summary>
        /// <param name="text"></param>
        /// <returns> A string of binary numbers</returns>
        public static string ISO88591Encode(string text)
        {
            byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(text);
            string[] byteStrings = new string[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                byteStrings[i] = Convert.ToString(bytes[i], 2).PadLeft(8, '0');
            }
            return string.Join("", byteStrings);
        }


    }
}