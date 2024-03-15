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
    public SupportedEncodingMode[] AllEncodingModes { get; } = (SupportedEncodingMode[])Enum.GetValues(typeof(SupportedEncodingMode));
    public ErrorCorrectionLevels[] AllErrorCorrectionLevels { get; } = (ErrorCorrectionLevels[])Enum.GetValues(typeof(ErrorCorrectionLevels));
    public int[] AllMasks { get; } = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
    public int[] AllVersions { get; } = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };

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
