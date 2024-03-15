using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace QRGenerator.ImageGenerator
{
    internal class ExportImage
    {

        /// <summary>
        /// Exporter le QR code vers une image .png avec option de scaling
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="scale"></param>
        public static void ExporterImage(bool?[,] qrCode, int scale = 100, string path = "qrcode.png", bool?[,]? patternToColor = null, SKColor? patternColor = null, string logoPath = null)
        {
            if (patternToColor is not null && patternColor is null)
            {
                throw new ArgumentException("If a pattern to color is provided, a color must be provided as well");
            }

            // ajouter un contour blanc (+ le mettre a l'echelle)
            int borderWidth = 5 * scale;

            // calcul des dimmensions
            var width = qrCode.GetLength(0) * scale + 2 * borderWidth;
            var height = qrCode.GetLength(1) * scale + 2 * borderWidth;
            var info = new SKImageInfo(width, height);

            // Créer la surface et clear le canva
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            // Dessiner le code QR
            for (int i = 0; i < qrCode.GetLength(0); i++)
            {
                for (int j = 0; j < qrCode.GetLength(1); j++)
                {
                    SKColor color;
                    if (patternToColor is not null && patternToColor[j, i] == true && patternColor is not null)
                    {
                        color = patternColor.Value;
                    }
                    else
                    {
                        color = qrCode[j, i] switch
                        {
                            true => SKColors.Black,
                            false => SKColors.White,
                            null => SKColors.Gray
                        };
                    }

                    // mettre à l'echelle
                    canvas.DrawRect(new SKRect((i * scale) + borderWidth, (j * scale) + borderWidth,
                        ((i + 1) * scale) + borderWidth, ((j + 1) * scale) + borderWidth),
                        new SKPaint { Color = color });
                }
            }

            // Exporter 
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(path);

            data.SaveTo(stream);
        }
    }
}