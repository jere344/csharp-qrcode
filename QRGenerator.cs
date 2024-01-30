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
    public EncoderController Encoder { get; set; }

    public string TextToEncode { get; set; }
    public int Version { get; set; }
    public QRCodeGenerator(string text, ErrorCorrectionLevels errorCorrectionLevel = ErrorCorrectionLevels.L)
    {
        TextToEncode = text;

        Encoder = new EncoderController(text, errorCorrectionLevel);
    }
}

