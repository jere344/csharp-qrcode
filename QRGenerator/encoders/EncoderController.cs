namespace QRGenerator.encoders;

public class EncoderController
{
    public SupportedEncodingMode EncodingMode { get; set; }
    public ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
    public int Version { get; set; }
    public string TextToEncode { get; set; }
    public string EncodedText { get; set; }

    //def convert_to_csharp(input_dict):
    //csharp_code = "{"
    //for key, value in input_dict.items():
    //    inner_dict = ", ".join([f'{{"{inner_key}", {inner_value}}}' for inner_key, inner_value in value.items()])
    //    csharp_code += f'{{{key}, new Dictionary<string, int> {{{inner_dict}}}}}, '
    //csharp_code = csharp_code[:-2]  # Remove the trailing comma and space
    //csharp_code += "}"
    //return csharp_code
    private Dictionary<int, Dictionary<string, int>> DataCodeWordCount = Static.DataCodeWordCount;

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
    // (Et c# est incapable d'avoir une sytaxe propre pour d�clarer des dictionnaires)
    // VersionLimitTable[ErrorLevel:str][EncodingMode:str] = MaxPossibleBitsEncode:int
    private Dictionary<int, Dictionary<string, Dictionary<string, int>>> VersionLimitTable = Static.VersionLimitTable;

    /// <summary>
    /// Create a new EncoderController and encode the input text.
    /// </summary>
    /// <param name="textToEncode"></param>
    /// <param name="errorCorrectionLevel"></param>
    /// <param name="version"></param>
    /// <param name="encodingMode"></param>
    /// <exception cref="Exception"></exception>
    public EncoderController(string textToEncode, ErrorCorrectionLevels errorCorrectionLevel, int? version = null, SupportedEncodingMode? encodingMode = null)
    {
        this.ErrorCorrectionLevel = errorCorrectionLevel;
        this.TextToEncode = textToEncode;

        if (encodingMode == null)
        {
            this.EncodingMode = ChooseEncoder();
        }
        else
        {
            this.EncodingMode = (SupportedEncodingMode)encodingMode;
        }
        
        if (version == null)
        {
            this.Version = CalculateVersion();
        }
        else
        {
            if (version < 1 || version > 40)
            {
                throw new Exception("Invalid version number");
            }
            this.Version = Math.Max(CalculateVersion(), (int)version);
        }

        this.EncodedText = Encode();
    }

    /// <summary>
    /// Encode the input text using the appropriate encoding mode
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private string Encode()
    {
        int padding = CharacterCountPadding();
        // Count the number of characters in the original input text, then convert that number into binary.The length of the character count indicator depends on the encoding mode and the QR code version that will be in use.To make the binary string the appropriate length, pad it on the left with 0s.
        string characterCountIndicator = Convert.ToString(TextToEncode.Length, 2).PadLeft(padding, '0');
        switch (EncodingMode)
        {
            case SupportedEncodingMode.Numeric:
                {
                    string ModeIndicator = "0001";
                    string encodedText = NumericEncoder.NumericEncode(TextToEncode);

                    return PadEncodedText(ModeIndicator + characterCountIndicator + encodedText);
                }

            case SupportedEncodingMode.Alphanumeric:
                {
                    string ModeIndicator = "0010";
                    string encodedText = AlphanumericEncoder.AlphanumericEncode(TextToEncode);

                    return PadEncodedText(ModeIndicator + characterCountIndicator + encodedText);
                }

            case SupportedEncodingMode.Byte:
                {
                    string ModeIndicator = "0100";
                    string encodedText = ByteEncoder.ByteEncode(TextToEncode);

                    return PadEncodedText(ModeIndicator + characterCountIndicator + encodedText);
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

    /// <summary>
    /// Calculate the minimal version of the QR code needed to encode the input string
    /// </summary>
    /// <returns> The minimal version number for the QR code or 0 if the input string is too long/returns>
    private int CalculateVersion()
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
    private int CharacterCountPadding()
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

    private string PadEncodedText(string encodedText)
    {
        string ErrLevel = Enum.GetName(typeof(ErrorCorrectionLevels), this.ErrorCorrectionLevel) ?? "L";
        //Determine the Required Number of Bits for this QR Code
        int requiredBits = DataCodeWordCount[Version][ErrLevel] * 8;
        // Console.WriteLine("Required bits : " + requiredBits);

        // Add a Terminator of 0s if Necessary
        //If the bit string is shorter than the total number of required bits, a terminator of up to four 0s must be added to the right side of the string.
        if (encodedText.Length < requiredBits)
        {
            int terminatorLength = Math.Min(4, requiredBits - encodedText.Length);
            encodedText += new string('0', terminatorLength);
            // Console.WriteLine("Terminator length : " + terminatorLength);
        }

        // Add More 0s to Make the Length a Multiple of 8
        //After adding the terminator, if the number of bits in the string is not a multiple of 8, first pad the string on the right with 0s to make the string's length a multiple of 8. 
        if (encodedText.Length % 8 != 0)
        {
            // Console.WriteLine("Padding length : " + (8 - encodedText.Length % 8));
            encodedText += new string('0', 8 - encodedText.Length % 8);
        }


        // Add Pad Bytes if the String is Still too Short
        //If the string is still not long enough to fill the maximum capacity, add the following bytes to the end of the string, repeating until the string has reached the maximum length: 11101100 00010001
        if (encodedText.Length < requiredBits)
        {
            string padBytes1 = "11101100";
            string padBytes2 = "00010001";
            int counter = 0;
            while (encodedText.Length < requiredBits)
            {
                if (counter % 2 == 0)
                {
                    encodedText += padBytes1;
                }
                else
                {
                    encodedText += padBytes2;
                }
                counter++;
            }
            // Console.WriteLine("Pad bytes added : " + counter);
        }

        // Console.WriteLine("Encoded text length : " + encodedText.Length);
        // Console.WriteLine("Requiered length : " + requiredBits);

        return encodedText;

    }

}
