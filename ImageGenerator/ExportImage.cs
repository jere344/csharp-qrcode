using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace QRGenerator.ImageGenerator
{
    internal class ExportImage
    {


        public static void ExporterImage(bool?[,] qrCode, int scale = 50)

        {
            // ajouter border (et scaler)
            int borderWidth = 5 * scale;

            // calcul dimmensions
            var width = qrCode.GetLength(0) * scale + 2 * borderWidth;
            var height = qrCode.GetLength(1) * scale + 2 * borderWidth;
            var info = new SKImageInfo(width, height);

            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;

            // clear 
            canvas.Clear(SKColors.White);

            

            // dessinner l'array
            for (int i = 0; i < qrCode.GetLength(0); i++)
            {
                for (int j = 0; j < qrCode.GetLength(1); j++)
                {
                    var color = qrCode[i, j] switch
                    {
                        true => SKColors.Black,
                        false => SKColors.White,
                        null => SKColors.Gray
                    };

                    // mettre a l'echelle
                    canvas.DrawRect(new SKRect((i * scale) + borderWidth, (j * scale) + borderWidth, ((i + 1) * scale) + borderWidth, ((j + 1) * scale) + borderWidth), new SKPaint { Color = color });
                }
            }

            // exporter 
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite("qrcode.png");

            data.SaveTo(stream);
        }

    }
}