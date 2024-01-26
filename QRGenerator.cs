using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;


namespace QRGenerator;

public class QRCodeGenerator
// qr generator class without any dependencies
{
    public EncoderController Encoder { get; set; }

    public string TextToEncode { get; set; }
    public int Version { get; set; }
    public QRCodeGenerator(string text, ErrorCorrectionLevels errorCorrectionLevel = ErrorCorrectionLevels.L)
    {
        TextToEncode = text;

        Encoder = new EncoderController(errorCorrectionLevel);
        Encoder.ChooseEncoder(text);
    }
    /// <summary>
    /// Calculates the minimum version needed to encode the text
    /// </summary>
    public void CalculateVersion()
    {
        int length = TextToEncode.Length;
 
    }

    // python to csharp dict converter :

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

    
    // J'ai banni ce dictionnaire dans un autre fichier parce que vstudio est trop stupide pour ignorer le type hint, et ca fait lag l'IDE.
    // (Et c# est incapable d'avoir une sytaxe propre pour déclarer des dictionnaires)
    public Dictionary<int, Dictionary<string, Dictionary<string, int>>> VersionLimitTable = Static.VersionLimitTable;
}

