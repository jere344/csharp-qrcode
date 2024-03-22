using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;
using QRGenerator.ImageGenerator;
using SkiaSharp;

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
    private QrErrorEncoder ReedEncoder { get; set; }
    public int Size { get; set; }
    public bool?[,] Matrix { get; set; }
    public bool?[,] MetadataMatrix { get; set; }
    public int? Mask { get; set; }

    public QRCodeGenerator(string text, ErrorCorrectionLevels errorCorrectionLevel = ErrorCorrectionLevels.L, int? version = null, SupportedEncodingMode? encodingMode = null, int? mask = null)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("The text to encode cannot be null or empty");
        }
        if (version is not null && (version < 1 || version > 40))
        {
            throw new ArgumentException("The version must be between 1 and 40");
        }
        if (encodingMode is not null && !Enum.IsDefined(typeof(SupportedEncodingMode), encodingMode))
        {
            throw new ArgumentException("The encoding mode is not supported");
        }
        if (!Enum.IsDefined(typeof(ErrorCorrectionLevels), errorCorrectionLevel))
        {
            throw new ArgumentException("The error correction level is not supported");
        }
        if (Mask is not null && (Mask < 0 || Mask > 7))
        {
            throw new ArgumentException("The mask must be between 0 and 7");
        }


        TextToEncode = text;

        this.Mask = mask;
        this.Encoder = new EncoderController(text, errorCorrectionLevel, version, encodingMode);
        this.Version = Encoder.Version;
        this.EncodingMode = Encoder.EncodingMode;
        this.ErrorCorrectionLevel = errorCorrectionLevel;
        this.EncodedText = Encoder.EncodedText;
        this.Size = Version * 4 + 17;

        this.ReedEncoder = new QrErrorEncoder(errorCorrectionLevel, Encoder.Version, EncodedText);
        this.SolomonEncoded = ReedEncoder.EncodedData;


        MetadataMatrix = new MatrixGenerator(this.Size).Matrix;
        MetadataMatrix = QrMetadataPlacer.AddAllMetadata(MetadataMatrix, Version);
        bool?[,] dataMatrix = new MatrixGenerator(this.Size).Matrix;
        dataMatrix = QrDataFiller.FillMatrix(dataMatrix, MetadataMatrix, SolomonEncoded);


        List<bool?[,]> maskedMatrices = GetAllMaskedMatrices(MetadataMatrix, dataMatrix, Version);

        if (Mask is not null)
        {
            Matrix = maskedMatrices[(int)Mask];
        }
        else
        {
            Matrix = QrApplyMask.GetBestMatrice(maskedMatrices);
        }
    }

    /// <summary>
    /// Get a list of the 8 possible masked matrices
    /// </summary>
    /// <param name="metadataMatrix"></param>
    /// <param name="dataMatrix"></param>
    /// <param name="version"></param>
    /// <returns> A list of 8 masked matrices</returns>
    public List<bool?[,]> GetAllMaskedMatrices(bool?[,] metadataMatrix, bool?[,] dataMatrix, int version)
    {
        List<bool?[,]> maskedMatrices = new List<bool?[,]>();
        for (int _mask = 0; _mask < 8; _mask++)
        {
            Matrix = new MatrixGenerator(this.Size).Matrix;

            // #First we create a clone of the data matrix with the mask applied
            if (dataMatrix.Clone() is not bool?[,] clonedDataMatrix)
            {
                throw new Exception("Cloning failed");
            }
            var maskedDataMatrix = QrApplyMask.ApplyMask(clonedDataMatrix, _mask);


            // # Then we add the metadata to the masked matrix

            // With this we can apply the mask to the data only and keep the metadata as is
            for (int i = 0; i < metadataMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < metadataMatrix.GetLength(1); j++)
                {
                    // If a cell is empty in the metadata matrix, we fill it with the data matrix
                    Matrix[i, j] = metadataMatrix[i, j] ?? maskedDataMatrix[i, j];

                }
            }

            // # Then we add the format information to the matrix
            var FormatString = Static.FormatInformationStrings[(ErrorCorrectionLevel.ToString()[0], _mask)];
            bool[] formatStringBool = FormatString.Select(x => x == '1').ToArray();
            Matrix = QrMetadataPlacer.AddFormatInformation(Matrix, formatStringBool);

            if (version > 6)
            {
                var VersionString = Static.VersionInformationStrings[Version];
                bool[]? versionStringBool = VersionString.Select(x => x == '1').ToArray();
                Matrix = QrMetadataPlacer.AddVersionInformation(Matrix, versionStringBool);
            }

            // # Finally we have a complete qr code with the mask applied
            maskedMatrices.Add(Matrix);
        }
        // We can use this list to compare the penalties of each mask and choose the best one
        return maskedMatrices;
    }

    /// <summary>
    /// Export the QR code as an image
    /// </summary>
    /// <param name="scale">The scale of the image</param>
    /// <param name="path">The path to save the image</param>
    /// <param name="patternColor">The color of the finders and alignements patterns</param>
    /// <param name="logoPath">The path to the logo</param>
    /// <param name="logoShadowType">The type of shadow to apply to the logo</param>
    public void ExportImage(int scale = 50, string path = "qrcode.png", SKColor? patternColor = null, string? logoPath = null, string logoShadowType = "circle", SKColor? backgroundColor = null)
    {
        if (Matrix is null)
        {
            throw new Exception("Matrix is null");
        }

        if (patternColor is not null)
        {
            bool?[,]? patternToColor = new MatrixGenerator(this.Size).Matrix;
            patternToColor = QrMetadataPlacer.AddAllFinderPatterns(patternToColor);
            patternToColor = QrMetadataPlacer.AddAlignmentPatterns(patternToColor, Version);
            ImageGenerator.ExportImage.ExporterImage(Matrix, scale, path, patternToColor, patternColor, logoPath, logoShadowType, backgroundColor);
        }
        else
        {
            ImageGenerator.ExportImage.ExporterImage(Matrix, scale, path, logoPath: logoPath, logoShadowType: logoShadowType, backgroundColor: backgroundColor);
        }
    }

    /// <summary>
    /// Print the matrix to the console
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void PrintMatrix()
    {
        if (Matrix is null)
        {
            throw new Exception("Matrix is null");
        }
        for (int i = 0; i < Matrix.GetLength(0); i++)
        {
            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Console.Write(Matrix[i, j] switch
                {
                    true => "■ ",
                    false => "  ",
                    null => "- ",
                });
            }
            Console.WriteLine();
        }
    }
}

