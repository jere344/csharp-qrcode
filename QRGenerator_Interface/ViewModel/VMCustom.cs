using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using QRGenerator_Interface.Model;
using QRGenerator;
using QRGenerator.encoders;
using System;
using System.Linq;

namespace QRGenerator_Interface.ViewModel
{
    public class VMCustom : ObservableObject
    {
        private readonly CustomModel _customModel;

        public int Scale
        {
            get => _customModel.Scale;
            set => _customModel.Scale = value;
        }

        public string Path
        {
            get => _customModel.Path;
            set => _customModel.Path = value;
        }

        public string? PatternColor
        {
            get => _customModel.PatternColor;
            set => _customModel.PatternColor = value;
        }

        public string? LogoPath
        {
            get => _customModel.LogoPath;
            set => _customModel.LogoPath = value;
        }

        public bool CircleShadow
        {
            get => _customModel.CircleShadow;
            set => _customModel.CircleShadow = value;
        }

        public bool ProjectedShadow
        {
            get => _customModel.ProjectedShadow;
            set => _customModel.ProjectedShadow = value;
        }

        public string? BackgroundColor
        {
            get => _customModel.BackgroundColor;
            set => _customModel.BackgroundColor = value;
        }

        public string? LastExportedPath
        {
            get => _customModel.LastExportedPath;
        }


        public VMCustom(QRCodeGenerator qr, string path, int scale)
        {
            path = path.Replace(".png", "-custom.png");
            _customModel = new CustomModel(qr, path, scale, "", "", "", false, false);
        }

        public string? ExportImage()
        {
            OnPropertyChanged(nameof(LogoPath));
            try
            {
                _customModel.ExportImage();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return null;
        }
    }
}
