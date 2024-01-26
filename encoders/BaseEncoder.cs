namespace QRGenerator.encoders;

public class BaseEncoder
{
    public string Encode(string text, EncodingMode mode)
    {
        switch (mode)
        {
            case EncodingMode.Numeric:
                var numericBaseEncoder = new NumericBaseEncoder();
                return string.Join("", numericBaseEncoder.NumericEncode(text));
            case EncodingMode.Alphanumeric:
                var alphanumericBaseEncoder = new AlphanumericBaseEncoder();
                return string.Join("", alphanumericBaseEncoder.AlphanumericEncode(text));
            case EncodingMode.Byte:
                var byteBaseEncoder = new ByteBaseEncoder();
                return string.Join("", byteBaseEncoder.ByteEncode(text));
            case EncodingMode.Kanji:
                var kanjiBaseEncoder = new KanjiBaseEncoder();
                return string.Join("", kanjiBaseEncoder.KanjiEncode(text));
            default:
                return "";
        }
    }

    public SupportedEncodingMode ChooseEncoder(string text)
    {
        // If the input string only consists of decimal digits (0 through 9), use numeric mode.
        if (text.All(char.IsDigit))
        {
            return SupportedEncodingMode.Numeric;
        }
        // if all of the characters in the input string can be found in the left column of the alphanumeric table, use alphanumeric mode. Lowercase letters CANNOT be encoded in alphanumeric mode; only uppercase
        else if (text.All(c => AlphanumericBaseEncoder.AlphanumericTable.ContainsKey(c.ToString().ToUpper())))
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
