using QRGenerator;
using QRGenerator.encoders;
using System;

namespace QRGenerator_Interface.Model;

public class GenerationModel
{
    public string TextToConvert { get; set; }
    public int? Version { get; set; }
    public ErrorCorrectionLevels? ErrorCorrectionLevel { get; set; }
    public int? Mask { get; set; }
    public SupportedEncodingMode? EncodingMode { get; set; }
    public string SaveFolder { get; set; }
    public string FileName { get; set; }
    public int Scale { get; set; }

    public GenerationModel(string textToConvert, int? version, ErrorCorrectionLevels? errorCorrectionLevels, int? mask, SupportedEncodingMode? encodingMode, string saveFolder, string fileName, int scale)
    {
        TextToConvert = textToConvert;
        Version = version;
        ErrorCorrectionLevel = errorCorrectionLevels;
        Mask = mask;
        EncodingMode = encodingMode;
        SaveFolder = saveFolder;
        FileName = fileName;
        Scale = scale;
    }

    public void GenerateQRCode()
    {
        if (string.IsNullOrEmpty(TextToConvert))
        {
            throw new ArgumentException("The text to encode cannot be null or empty");
        }

        QRCodeGenerator qr;

        if (ErrorCorrectionLevel is null)
        {
            qr = new QRCodeGenerator(TextToConvert, version: Version, encodingMode: EncodingMode, mask: Mask);
        }
        else
        {
            qr = new QRCodeGenerator(TextToConvert, ErrorCorrectionLevel.Value, Version, EncodingMode, Mask);
        }
        qr.ExportImage(Scale, SaveFolder + FileName);
    }
}
