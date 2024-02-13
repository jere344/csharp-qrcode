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

        public static void ExporterImage(bool?[,] qrCode)
        {
            // crate a surface
            var info = new SKImageInfo(qrCode.GetLength(0), qrCode.GetLength(1));

            using var surface = SKSurface.Create(info);

            // the the canvas and properties
            var canvas = surface.Canvas;

            // make sure the canvas is blank
            canvas.Clear(SKColors.Gray);


            for (int i = 0; i < qrCode.GetLength(0); i++)
            {
                for (int j = 0; j < qrCode.GetLength(1); j++)
                {
                    switch (qrCode[j, i])
                    {
                        case true:
                            canvas.DrawPoint(i, j, SKColors.Black);
                            break;

                        case false:
                            canvas.DrawPoint(i, j, SKColors.White);
                            break;

                        case null:
                            { canvas.DrawPoint(i, j, SKColors.Gray); }
                            break;
                    }
                }
            }

            // save the file
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite("output.png");

            data.SaveTo(stream);
        }
    }
}
