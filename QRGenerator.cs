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
    public string FormatString { get; set; }
    public string? VersionString { get; set; }
    
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

        bool?[,] metadataMatrix = new MatrixGenerator(21).Matrix;
        metadataMatrix = QrMetadataPlacer.AddAllMetadata(metadataMatrix, Version);

        bool?[,] dataMatrix = new MatrixGenerator(21).Matrix;
        dataMatrix = QrDataFiller.FillMatrix(dataMatrix, metadataMatrix, SolomonEncoded);

        dataMatrix = QrApplyMask.ApplyMask(dataMatrix, 0);

        Matrix = new MatrixGenerator(21).Matrix;
        // Combine the metadata and data matrix
        // With this we can apply the mask to the data only and keep the metadata as is
        for (int i = 0; i < metadataMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < metadataMatrix.GetLength(1); j++)
            {
                // If a cell is empty in the metadata matrix, we fill it with the data matrix
                Matrix[i, j] = metadataMatrix[i, j] ?? dataMatrix[i, j];

            }
        }
        FormatString = Static.FormatInformationStrings[(ErrorCorrectionLevel.ToString()[0], Version)];
        if (version > 6)
        {
            VersionString = Static.VersionInformationStrings[Version]; 
        }

    }
}

