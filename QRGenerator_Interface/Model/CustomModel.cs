using QRGenerator;
using SkiaSharp;
using System;

namespace QRGenerator_Interface.Model;

public class CustomModel
{
    public int Scale { get; set; }
    public string Path { get; set; }
    public string PatternColor { get; set; }
    public string LogoPath { get; set; }
    public string BackgroundColor { get; set; }
    public bool CircleShadow { get; set; }
    public bool ProjectedShadow { get; set; }
    public string LogoShadowType => (CircleShadow ? "circle+" : "") + (ProjectedShadow is true ? "shadow" : "");
    public string? LastExportedPath { get; set; }

    public QRCodeGenerator Qr;

    public CustomModel(QRCodeGenerator qr, string path, int scale, string patternColor, string logoPath, string backgroundColor, bool circleShadow, bool projectedShadow)
    {
        if (qr.ErrorCorrectionLevel != ErrorCorrectionLevels.H)
        {
            throw new ArgumentException("The error correction level must be H");
        }

        this.Qr = qr;
        Path = path;
        Scale = scale;
        PatternColor = patternColor;
        LogoPath = logoPath;
        BackgroundColor = backgroundColor;
        CircleShadow = circleShadow;
        ProjectedShadow = projectedShadow;
    }

    public void ExportImage()
    {
		SKColor? patternColor = PatternColor == "" ? null : SKColor.Parse(PatternColor);
		SKColor? backgroundColor = BackgroundColor == "" ? null : SKColor.Parse(BackgroundColor);

        LastExportedPath = Path.Replace(".png", $"-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.png");
        
        Qr.ExportImage(Scale, LastExportedPath, patternColor, LogoPath == "" ? null : LogoPath, LogoShadowType, backgroundColor);
    }
}