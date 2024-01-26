namespace QRGenerator.encoders;

public class Encoder
{
    public string Encode(string text, EncodingMode mode)
    {
        switch (mode)
        {
            case EncodingMode.Numeric:
                var numericEncoder = new NumericEncoder();
                return string.Join("", numericEncoder.NumericEncode(text));
            case EncodingMode.Alphanumeric:
                var alphanumericEncoder = new AlphanumericEncoder();
                return string.Join("", alphanumericEncoder.AlphanumericEncode(text));
            case EncodingMode.Byte:
                var byteEncoder = new ByteEncoder();
                return string.Join("", byteEncoder.ByteEncode(text));
            case EncodingMode.Kanji:
                var kanjiEncoder = new KanjiEncoder();
                return string.Join("", kanjiEncoder.KanjiEncode(text));
            default:
                return "";
        }
    }
}
