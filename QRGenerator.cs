using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QRGenerator;

public class QRCodeGenerator
// qr generator class without any dependencies
{

    public SupportedEncodingMode EncodingMode { get; set; }
    public string TextToEncode { get; set; }
    public QRCodeGenerator(string text)
    {
        EncodingMode = SelectEncoder(text);
        TextToEncode = text;
    }

    public SupportedEncodingMode SelectEncoder(string text)
    {
        // If the input string only consists of decimal digits (0 through 9), use numeric mode.
        if (text.All(char.IsDigit))
        {
            return SupportedEncodingMode.Numeric;
        }
        // if all of the characters in the input string can be found in the left column of the alphanumeric table, use alphanumeric mode. Lowercase letters CANNOT be encoded in alphanumeric mode; only uppercase
        else if (text.All(c => AlphanumericEncoder.AlphanumericTable.ContainsKey(c.ToString().ToUpper())))
        {
            return SupportedEncodingMode.Alphanumeric;
        }
        // Else use Byte mode, Kanji mode is not implemented yet
        else
        {
            return SupportedEncodingMode.Byte;
        }

    }



}
