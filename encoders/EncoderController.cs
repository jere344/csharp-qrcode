namespace QRGenerator.encoders;

public class EncoderController
{

    public EncoderController(string text)
    {
        EncodingMode = ChooseEncoder(text);
    }
    public SupportedEncodingMode EncodingMode { get; set; }
    public string Encode(string text)
    {
        switch (EncodingMode)
        {
            case EncodingMode.Numeric:
                var numericEncoderController = new NumericEncoderController();
                return string.Join("", numericEncoderController.NumericEncode(text));
            case EncodingMode.Alphanumeric:
                var alphanumericEncoderController = new AlphanumericEncoderController();
                return string.Join("", alphanumericEncoderController.AlphanumericEncode(text));
            case EncodingMode.Byte:
                var byteEncoderController = new ByteEncoderController();
                return string.Join("", byteEncoderController.ByteEncode(text));
            case EncodingMode.Kanji:
                var kanjiEncoderController = new KanjiEncoderController();
                return string.Join("", kanjiEncoderController.KanjiEncode(text));
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
        else if (text.All(c => AlphanumericEncoderController.AlphanumericTable.ContainsKey(c.ToString().ToUpper())))
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
