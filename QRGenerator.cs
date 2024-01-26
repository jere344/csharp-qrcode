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
    public BaseEncoder Encoder = new BaseEncoder();

    public string TextToEncode { get; set; }
    public SupportedEncodingMode EncodingMode { get; set; }
    public QRCodeGenerator(string text)
    {
        TextToEncode = text;
        EncodingMode = Encoder.ChooseEncoder(text);
    }

}
