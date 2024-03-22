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
        public static void ExporterImage(bool?[,] qrCode, int scale = 100, string path = "qrcode.png", bool?[,]? patternToColor = null, SKColor? patternColor = null, string? logoPath = null, string logoShadowType = "circle", SKColor? backgroundColor = null)
        {
            if (patternToColor is not null && patternColor is null)
            {
                throw new ArgumentException("If a pattern to color is provided, a color must be provided as well");
            }

            if (logoPath is not null && !System.IO.File.Exists(logoPath))
            {
                throw new ArgumentException("The logo path is invalid");
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
                            false => backgroundColor ?? SKColors.White,
                            null => SKColors.Gray
                        };
                    }

                    // mettre à l'echelle
                    canvas.DrawRect(new SKRect((i * scale) + borderWidth, (j * scale) + borderWidth,
                        ((i + 1) * scale) + borderWidth, ((j + 1) * scale) + borderWidth),
                        new SKPaint { Color = color });
                }
            }

            if (logoPath is not null)
            {
                canvas = AddLogo(canvas, logoPath, scale, width, height, logoShadowType, qrCode);
            }

            // Exporter 
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(path);

            data.SaveTo(stream);
        }
        private static SKCanvas AddLogo(SKCanvas canvas, string logoPath, int scale, int width, int height, string logoShadowType, bool?[,] qrCode)
        {
            using var logo = SKBitmap.Decode(logoPath);

            // fait en sorte que le logo soit 1/3 de la taille du QR code
            int logoWidth = (int)(qrCode.GetLength(0) * scale / 3);
            int logoHeight = (int)(qrCode.GetLength(1) * scale / 3);

            int logoWidthWhite = (int)(logoWidth * 1.1);
            int logoHeightWhite = (int)(logoHeight * 1.1);

            if (logoShadowType.Contains("shadow"))
            {
                // Dessine une ombre blanche du logo légèrement plus grande
                var whiteLogo = new SKBitmap(logo.Width, logo.Height);
                for (int i = 0; i < logo.Width; i++)
                {
                    for (int j = 0; j < logo.Height; j++)
                    {
                        var pixel = logo.GetPixel(i, j);
                        if (pixel.Alpha != 0)
                        {
                            whiteLogo.SetPixel(i, j, SKColors.White);
                        }
                    }
                }

                canvas.DrawBitmap(whiteLogo, new SKRect(width / 2 - logoWidthWhite / 2, height / 2 - logoHeightWhite / 2, width / 2 + logoWidthWhite / 2, height / 2 + logoHeightWhite / 2));
            }

            if (logoShadowType.Contains("circle"))
            {
                // Dessine un cercle blanc autour du logo
                canvas.DrawCircle(width / 2, height / 2, logoWidthWhite / 2, new SKPaint { Color = SKColors.White });
            }

            // Dessine le logo
            canvas.DrawBitmap(logo, new SKRect(width / 2 - logoWidth / 2, height / 2 - logoHeight / 2, width / 2 + logoWidth / 2, height / 2 + logoHeight / 2));

            return canvas;
        }

    }
}