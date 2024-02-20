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
        if (mask is not null && (mask < 0 || mask > 7))
        {
            throw new ArgumentException("The mask must be between 0 and 7");
        }


        TextToEncode = text;

        this.Encoder = new EncoderController(text, errorCorrectionLevel, version, encodingMode);
        this.Version = Encoder.Version;
        this.EncodingMode = Encoder.EncodingMode;
        this.ErrorCorrectionLevel = errorCorrectionLevel;
        this.EncodedText = Encoder.EncodedText;
        // Console.WriteLine("Version: " + Version);
        // Console.WriteLine("Encoding mode: " + EncodingMode);
        // Console.WriteLine("Error correction level: " + ErrorCorrectionLevel);
        // Console.WriteLine("Encoded text length: " + EncodedText.Length);
        // Console.WriteLine("Encoded text : " + EncodedText);
        this.Size = Version * 4 + 17;

        this.ReedEncoder = new QrErrorEncoder(errorCorrectionLevel, Encoder.Version, EncodedText);
        this.SolomonEncoded = ReedEncoder.EncodedData;
        // Console.WriteLine("Solomon encoded: " + string.Join(", ", SolomonEncoded));


        bool?[,] metadataMatrix = new MatrixGenerator(this.Size).Matrix;
        metadataMatrix = QrMetadataPlacer.AddAllMetadata(metadataMatrix, Version);
        bool?[,] dataMatrix = new MatrixGenerator(this.Size).Matrix;
        dataMatrix = QrDataFiller.FillMatrix(dataMatrix, metadataMatrix, SolomonEncoded);



        if (mask is not null)
        {
            Matrix = QrApplyMask.ApplyMask(dataMatrix, mask.Value);
        }
        else
        {
            List<bool?[,]> maskedMatrices = GetAllMaskedMatrices(metadataMatrix, dataMatrix, Version);
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

    /// <summary>
    /// Export the QR code as an image
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="Exception"></exception>
    public void ExportImage(string path)
    {
        if (Matrix is null)
        {
            throw new Exception("Matrix is null");
        }
        ImageGenerator.ExportImage.ExporterImage(Matrix);
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
                    true => "1 ",
                    false => "0 ",
                    null => "- ",
                });
            }
            Console.WriteLine();
        }
    }
}

