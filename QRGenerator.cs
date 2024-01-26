using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;


namespace QRGenerator;

public class QRCodeGenerator
// qr generator class without any dependencies
{
    public EncoderController Encoder { get; set; }

    public string TextToEncode { get; set; }
    public QRCodeGenerator(string text)
    {
        TextToEncode = text;
        Encoder = new EncoderController();
        Encoder.ChooseEncoder(text);
    }

}
