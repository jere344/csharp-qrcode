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
    public class VMGeneration : ObservableObject
    {
        private readonly GenerationModel _generationModel;

        public string TextToConvert
        {
            get => _generationModel.TextToConvert;
            set => _generationModel.TextToConvert = value;
        }

        public int? Version
        {
            get => _generationModel.Version;
            set
            {
                if (value < 1 || value > 40)
                {
                    _generationModel.Version = null;
                }
                else
                {
                    _generationModel.Version = value;
                }
            }
        }

        public ErrorCorrectionLevels? ErrorCorrectionLevel
        {
            get => _generationModel.ErrorCorrectionLevel;
            set => _generationModel.ErrorCorrectionLevel = value;
        }

        public int? Mask
        {
            get => _generationModel.Mask;
            set
            {
                if (value < 0 || value > 7)
                {
                    _generationModel.Mask = null;
                }
                else
                {
                    _generationModel.Mask = value;
                }
            }
        }

        public SupportedEncodingMode? EncodingMode
        {
            get => _generationModel.EncodingMode;
            set => _generationModel.EncodingMode = value;
        }

        public string SaveFolder
        {
            get => _generationModel.SaveFolder;
            set => _generationModel.SaveFolder = value;
        }

        public string FileName
        {
            get => _generationModel.FileName;
            set => _generationModel.FileName = value;
        }

        public int Scale
        {
            get => _generationModel.Scale;
            set
            {
                if (value < 1 || value > 100)
                {
                    _generationModel.Scale = 1;
                }
                else
                {
                    _generationModel.Scale = value;
                }
                OnPropertyChanged(nameof(Scale));
            }
        }


        public VMGeneration()
        {
            _generationModel = new GenerationModel("", null, null, null, null, "", "", 10);
        }

        public string? GenerateQRCode()
        {
            var result = _generationModel.GenerateQRCode();
            OnPropertyChanged(nameof(CanOpenCustomizationWindow));
            OnPropertyChanged(nameof(GetLastGeneratedQRCode));
            OnPropertyChanged(nameof(SavePath));
            OnPropertyChanged(nameof(CanOpenCustomizationWindow));
            return result;
        }

        public IEnumerable<SupportedEncodingMode> AllEncodingModes => _generationModel.AllEncodingModes;
        public IEnumerable<ErrorCorrectionLevels> AllErrorCorrectionLevels => _generationModel.AllErrorCorrectionLevels;
        public IEnumerable<int> AllMasks => _generationModel.AllMasks;
        public IEnumerable<int> AllVersions => _generationModel.AllVersions;

        public bool _AutoVersion { get; set; } = true;
        public bool AutoVersion
        {
            get => _AutoVersion;
            set
            {
                _AutoVersion = value;
                if (value)
                {
                    Version = null;
                }
                OnPropertyChanged(nameof(Version));
                OnPropertyChanged(nameof(IsNotAutoVersion));
            }
        }
        public bool IsNotAutoVersion => !_AutoVersion;
        
        public bool _AutoMask { get; set; } = true;
        public bool AutoMask
        {
            get => _AutoMask;
            set
            {
                _AutoMask = value;
                if (value)
                {
                    Mask = null;
                }
                OnPropertyChanged(nameof(Mask));
                OnPropertyChanged(nameof(IsNotAutoMask));
            }
        }
        public bool IsNotAutoMask => !_AutoMask;

        public bool _AutoErrorCorrectionLevel { get; set; } = true;
        public bool AutoErrorCorrectionLevel
        {
            get => _AutoErrorCorrectionLevel;
            set
            {
                _AutoErrorCorrectionLevel = value;
                if (value)
                {
                    ErrorCorrectionLevel = null;
                }
                OnPropertyChanged(nameof(ErrorCorrectionLevel));
                OnPropertyChanged(nameof(IsNotAutoErrorCorrectionLevel));
            }
        }
        public bool IsNotAutoErrorCorrectionLevel => !_AutoErrorCorrectionLevel;

        public bool _AutoEncodingMode { get; set; } = true;
        public bool AutoEncodingMode
        {
            get => _AutoEncodingMode;
            set
            {
                _AutoEncodingMode = value;
                if (value)
                {
                    EncodingMode = null;
                }
                OnPropertyChanged(nameof(EncodingMode));
                OnPropertyChanged(nameof(IsNotAutoEncodingMode));
            }
        }
        public bool IsNotAutoEncodingMode => !_AutoEncodingMode;

        public bool _AutoScale { get; set; } = true;
        public bool AutoScale
        {
            get => _AutoScale;
            set
            {
                _AutoScale = value;
                if (value)
                {
                    Scale = 10;
                }
                OnPropertyChanged(nameof(Scale));
                OnPropertyChanged(nameof(IsNotAutoScale));
            }
        }
        public bool IsNotAutoScale => !_AutoScale;

        public bool CanOpenCustomizationWindow => _generationModel.LastGeneratedQRCode is not null;
        public QRCodeGenerator? GetLastGeneratedQRCode => _generationModel.LastGeneratedQRCode;
    
        public string SavePath => _generationModel.SavePath;
    }
}
