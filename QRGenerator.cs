using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;


namespace QRGenerator;

public class QRCodeGenerator
{
    private EncoderController Encoder { get; set; }
    public ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
    public SupportedEncodingMode EncodingMode { get; set; }
    public string TextToEncode { get; set; }
    public int Version { get; set; }
    public string EncodedText { get; set; }
    public List<string> SolomonEncoded = new List<string>();
    public QRCodeGenerator(string text, ErrorCorrectionLevels errorCorrectionLevel = ErrorCorrectionLevels.L, int? version=null, SupportedEncodingMode? encodingMode = null)
    {
        TextToEncode = text;

        this.Encoder = new EncoderController(text, errorCorrectionLevel, version, encodingMode);
        this.Version = Encoder.Version;
        this.EncodingMode = Encoder.EncodingMode;
        this.ErrorCorrectionLevel = errorCorrectionLevel;
        this.EncodedText = Encoder.EncodedText;



        // List<Group>
        //     List<Block>
        //         List<Codeword>
        //             ~50% donn�es
        //             ~50% correction d'erreur'


    }
}

