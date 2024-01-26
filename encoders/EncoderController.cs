using QRGenerator;
namespace QRGenerator.encoders;

public class EncoderController
{
    public SupportedEncodingMode EncodingMode { get; set; }
    public ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
    public int Version { get; set; }

    public EncoderController(ErrorCorrectionLevels errorCorrectionLevel)
    {
        ErrorCorrectionLevel = errorCorrectionLevel;
    }

    public string[] Encode(string text)
    {
        switch (EncodingMode)
        {
            case SupportedEncodingMode.Numeric:
                return NumericEncoder.NumericEncode(text);
            case SupportedEncodingMode.Alphanumeric:
                return AlphanumericEncoder.AlphanumericEncode(text);
            case SupportedEncodingMode.Byte:
                return ByteEncoder.ByteEncode(text);
            case SupportedEncodingMode.Kanji:
                return KanjiEncoder.KanjiEncode(text);
            default:
                return ByteEncoder.ByteEncode(text);
        }
    }

    private SupportedEncodingMode _ChooseEncoder(string text)
    {
        // If the input string only consists of decimal digits (0 through 9), use numeric mode.
        if (text.All(char.IsDigit))
        {
            return SupportedEncodingMode.Numeric;
        }
        // if all of the characters in the input string can be found in the left column of the alphanumeric table, use alphanumeric mode. Lowercase letters CANNOT be encoded in alphanumeric mode; only uppercase
        else if (text.All(c => AlphanumericEncoder.AlphanumericTable.ContainsKey(c)))
        {
            return SupportedEncodingMode.Alphanumeric;
        }
        // Else use Byte mode, Kanji mode is not implemented yet
        else
        {
            return SupportedEncodingMode.Byte;
        }
    }

    public void ChooseEncoder(string text)
    {
        EncodingMode = _ChooseEncoder(text);
    }
}
