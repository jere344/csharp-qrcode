using QRGenerator;
using SkiaSharp;

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

    public QRCodeGenerator Qr;

    public CustomModel(QRCodeGenerator qr, string path, int scale, string patternColor, string logoPath, string backgroundColor, bool circleShadow, bool projectedShadow)
    {
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
        
        Qr.ExportImage(Scale, Path, patternColor, LogoPath == "" ? null : LogoPath, LogoShadowType, backgroundColor);
    }
}