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
        Console.WriteLine("Version: " + Version);
        this.EncodingMode = Encoder.EncodingMode;
        Console.WriteLine("Encoding mode: " + EncodingMode);
        this.ErrorCorrectionLevel = errorCorrectionLevel;
        Console.WriteLine("Error correction level: " + ErrorCorrectionLevel);
        this.EncodedText = Encoder.EncodedText;
        Console.WriteLine("Encoded text length: " + EncodedText.Length);
        Console.WriteLine("Encoded text : " + EncodedText);
        
        this.Size = Version * 4 + 17;

        this.ReedEncoder = new QrErrorEncoder(errorCorrectionLevel, Encoder.Version, EncodedText);
        this.SolomonEncoded = ReedEncoder.EncodedData;
        Console.WriteLine("Solomon encoded: " + string.Join(", ", SolomonEncoded));


        bool?[,] metadataMatrix = new MatrixGenerator(this.Size).Matrix;
        metadataMatrix = QrMetadataPlacer.AddAllMetadata(metadataMatrix, Version);

        bool?[,] dataMatrix = new MatrixGenerator(this.Size).Matrix;
        dataMatrix = QrDataFiller.FillMatrix(dataMatrix, metadataMatrix, SolomonEncoded);


        List<bool?[,]> maskedMatrices = GetMaskedMatrices(metadataMatrix, dataMatrix, Version);

        Matrix = QrApplyMask.GetBestMatrice(maskedMatrices);

        ImageGenerator.ExportImage.ExporterImage(Matrix);
    }

    public List<bool?[,]> GetMaskedMatrices(bool?[,] metadataMatrix, bool?[,] dataMatrix, int version)
    {
        List<bool?[,]> maskedMatrices = new List<bool?[,]>();
        for (int mask = 0; mask < 8; mask++)
        {
            Matrix = new MatrixGenerator(this.Size).Matrix;

            // #First we create a clone of the data matrix with the mask applied
            if (dataMatrix.Clone() is not bool?[,] clonedDataMatrix)
            {
                throw new Exception("Cloning failed");
            }
            var maskedDataMatrix = QrApplyMask.ApplyMask(clonedDataMatrix, mask);
            
           
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
            var FormatString = Static.FormatInformationStrings[(ErrorCorrectionLevel.ToString()[0], mask)];
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
}

