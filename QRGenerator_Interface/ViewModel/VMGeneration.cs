using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using QRGenerator_Interface.Model;
using QRGenerator;
using QRGenerator.encoders;

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
            set {
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
            set {
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
            set {
                if (value < 1 || value > 100)
                {
                    _generationModel.Scale = 1;
                }
                else
                {
                    _generationModel.Scale = value;
                }
            }
        }

        public ICommand GenerateQRCodeCommand { get; }

        public VMGeneration()
        {
            GenerateQRCodeCommand = new RelayCommand(GenerateQRCode);
            _generationModel = new GenerationModel("", null, null, null, null, "", "", 10);
        }

        private void GenerateQRCode()
        {
            _generationModel.GenerateQRCode();
        }
    }
}
