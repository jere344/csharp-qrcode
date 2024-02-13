using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;
using QRGenerator.ImageGenerator;


namespace QRGenerator;

public class QRCodeGenerator
{
    private EncoderController Encoder { get; set; }
    public ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
    public SupportedEncodingMode EncodingMode { get; set; }
    public string TextToEncode { get; set; }
    public int Version { get; set; }
    public string EncodedText { get; set; }
    public List<int> SolomonEncoded;
    public QrErrorEncoder ReedEncoder { get; set; }
    public int Size { get; set; }
    public bool?[,] Matrix { get; set; }
    public QRCodeGenerator(string text, ErrorCorrectionLevels errorCorrectionLevel = ErrorCorrectionLevels.L, int? version=null, SupportedEncodingMode? encodingMode = null)
    {
        TextToEncode = text;

        this.Encoder = new EncoderController(text, errorCorrectionLevel, version, encodingMode);
        this.Version = Encoder.Version;
        this.EncodingMode = Encoder.EncodingMode;
        this.ErrorCorrectionLevel = errorCorrectionLevel;
        this.EncodedText = Encoder.EncodedText;
        
        this.Size = Version * 4 + 17;

        this.ReedEncoder = new QrErrorEncoder(errorCorrectionLevel, Encoder.Version, EncodedText);
        this.SolomonEncoded = ReedEncoder.EncodedData;


        this.Matrix = new MatrixGenerator(21).Matrix;
        this.Matrix = QrMetadataPlacer.AddAllMetadata(this.Matrix);
        this.Matrix = QrDataFiller.FillMatrix(this.Matrix, SolomonEncoded);


    }
}

