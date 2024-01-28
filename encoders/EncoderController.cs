using QRGenerator;
using System;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace QRGenerator.encoders;

public class EncoderController
{
    public SupportedEncodingMode EncodingMode { get; set; }
    public ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
    public int Version { get; set; }
    public string TextToEncode { get; set; }

    public EncoderController(string text, ErrorCorrectionLevels errorCorrectionLevel)
    {
        ErrorCorrectionLevel = errorCorrectionLevel;
        TextToEncode = text;
        EncodingMode = ChooseEncoder();
        Version = CalculateVersion();
    }

    public string[] Encode(string text)
    {
        int padding = CharacterCountPadding();
        // Count the number of characters in the original input text, then convert that number into binary.The length of the character count indicator depends on the encoding mode and the QR code version that will be in use.To make the binary string the appropriate length, pad it on the left with 0s.
        string characterCountIndicator = Convert.ToString(text.Length, 2).PadLeft(padding, '0');
        switch (EncodingMode)
        {
            case SupportedEncodingMode.Numeric:
                {
                    string ModeIndicator = "0001";
                    string encodedText = string.Join("", NumericEncoder.NumericEncode(text));

                    return new string[] { ModeIndicator, characterCountIndicator, encodedText };
                }

            case SupportedEncodingMode.Alphanumeric:
                {
                    string ModeIndicator = "0010";
                    string encodedText = string.Join("", AlphanumericEncoder.AlphanumericEncode(text));

                    return new string[] { ModeIndicator, characterCountIndicator, string.Join("", encodedText) };
                }

            case SupportedEncodingMode.Byte:
                {
                    string ModeIndicator = "0100";
                    string[] encodedText = ByteEncoder.ByteEncode(text);

                    return new string[] { ModeIndicator, characterCountIndicator, string.Join("", encodedText) };
                }

            default:
                {
                    throw new Exception("Encoding mode not supported");
                }
        }
    }

    private SupportedEncodingMode ChooseEncoder()
    {
        // If the input string only consists of decimal digits (0 through 9), use numeric mode.
        if (TextToEncode.All(char.IsDigit))
        {
            return SupportedEncodingMode.Numeric;
        }
        // if all of the characters in the input string can be found in the left column of the alphanumeric table, use alphanumeric mode. Lowercase letters CANNOT be encoded in alphanumeric mode; only uppercase
        else if (TextToEncode.All(c => AlphanumericEncoder.AlphanumericTable.ContainsKey(c)))
        {
            return SupportedEncodingMode.Alphanumeric;
        }
        // Else use Byte mode, Kanji mode is not implemented yet
        else
        {
            return SupportedEncodingMode.Byte;
        }
    }


    // python dict to csharp dict converter :
    //def convert_to_csharp(input_dict):
    //csharp_code = "{"
    //for key, value in input_dict.items():
    //    csharp_code += "{" + str(key) + ", new Dictionary<string, Dictionary<string, int>> {"
    //    for inner_key, inner_value in value.items():
    //        inner_dict = ", ".join([f'{{"{k}", {v}}}' for k, v in inner_value.items()])
    //        csharp_code += f'{{"{inner_key}", new Dictionary<string, int> {{{inner_dict}}}}}, '
    //    csharp_code = csharp_code[:-2]  # Remove the trailing comma and space
    //    csharp_code += "}},"
    //csharp_code = csharp_code[:-1]  # Remove the trailing comma
    //csharp_code += "}"
    //return csharp_code


    // J'ai banni ce dictionnaire dans un autre fichier parce qu'il fait lag l'IDE.
    // (Et c# est incapable d'avoir une sytaxe propre pour déclarer des dictionnaires)
    // VersionLimitTable[ErrorLevel:str][EncodingMode:str] = MaxPossibleBitsEncode:int
    public Dictionary<int, Dictionary<string, Dictionary<string, int>>> VersionLimitTable = Static.VersionLimitTable;


    /// <summary>
    /// Calculate the minimal version of the QR code needed to encode the input string
    /// </summary>
    /// <returns> The minimal version number for the QR code or 0 if the input string is too long/returns>
    public int CalculateVersion()
    {
        int length = TextToEncode.Length;
        // Because the version limit dictionary use a string representation of the encoding mode, we need to convert the enum to a string
        string ErrLevel = Enum.GetName(typeof(ErrorCorrectionLevels), this.ErrorCorrectionLevel) ?? "L";
        string EncMode = (Enum.GetName(typeof(SupportedEncodingMode), this.EncodingMode) ?? "Byte")[..1]; // first letter of the encoding mode

        // We could optimize this by using a binary search algorithm, but it's not worth it
        for (int i = 1; i <= 40; i++)
        {
            if (length <= VersionLimitTable[i][ErrLevel][EncMode])
            {
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// Calculate the number of bits needed to encode the character count indicator for the current version and encoding mode
    /// </summary>
    /// <returns> The number of bits needed to encode the character count indicator</returns>
    public int CharacterCountPadding()
    {
        if (Version <= 9)
        {
            if (EncodingMode == SupportedEncodingMode.Numeric) { return 10; }
            else if (EncodingMode == SupportedEncodingMode.Alphanumeric) { return 9; }
            else { return 8; }
        }
        else if (Version <= 26)
        {
            if (EncodingMode == SupportedEncodingMode.Numeric) { return 12; }
            else if (EncodingMode == SupportedEncodingMode.Alphanumeric) { return 11; }
            else if (EncodingMode == SupportedEncodingMode.Byte) { return 16; }
            else { return 10; }
        }
        else
        {
            if (EncodingMode == SupportedEncodingMode.Numeric) { return 14; }
            else if (EncodingMode == SupportedEncodingMode.Alphanumeric) { return 13; }
            else if (EncodingMode == SupportedEncodingMode.Byte) { return 16; }
            else { return 12; }
        }
    }
}
