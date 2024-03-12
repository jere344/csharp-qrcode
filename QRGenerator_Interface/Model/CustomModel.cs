namespace QRGenerator_Interface.Model;

public class CustomModel
{
    public string? moduleReplacementPath { get; set; }
    public string? logoPath { get; set; }
    public string colorBackground { get; set; }
    public string colorPatterns { get; set; }

    public CustomModel(string? moduleReplacementPath, string? logoPath, string colorBackground, string colorPatterns)
    {
        this.moduleReplacementPath = moduleReplacementPath;
        this.logoPath = logoPath;
        this.colorBackground = colorBackground;
        this.colorPatterns = colorPatterns;
    }
}