using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;

namespace QRGenerator
{
    internal class ReedSolomonEncoder
    {
        //dico = {}
        //for line in data.splitlines():
        //    version = line.split("-")[0]
        //    if not version in dico:
        //        dico[version] = {}
        //    level = line.split("-")[1].split("\t")[0]
        //    if not level in dico[version]:
        //        dico[version][level] = {}
        //    dico[version][level] = [int(line.split("\t")[2] or 0), int (line.split("\t")[3] or 0), int (line.split("\t")[4] or 0), int (line.split("\t")[5] or 0), int (line.split("\t")[6] or 0)]

        //def convert_to_csharp(input_dict):
        //    csharp_code = "{"
        //    for key, value in input_dict.items():
        //        inner_dict = ", ".join([f'{{"{inner_key}", new int[5] {{{", ".join(map(str, inner_value))}}}}}' for inner_key, inner_value in value.items()])
        //        csharp_code += f'{{{key}, new Dictionary<string, int[]> {{{inner_dict}}}}}, '
        //    csharp_code = csharp_code[:-2]  # Remove the trailing comma and space
        //    csharp_code += "}"
        //    return csharp_code

        // [Version][CorrectionLevel] = [ECWordPerBlock, BlockGroup1, WordCount1, BlockGroup2, WordCount2]
        public static Dictionary<int, Dictionary<string, int[]>> ErrorCorrectionCodeWordCount = Static.ErrorCorrectionCodeWordCount;
        
        //dico = {}
        //for line in data.splitlines():
        //    dico[int(line.split("\t")[3] or 0)] = int (line.split("\t")[4] or 0)

        //def convert_to_csharp(input_dict):
        //    csharp_code = "{"
        //    for key, value in input_dict.items():
        //        csharp_code += f'{{{key}, {value}}}, '
        //    csharp_code = csharp_code[:-2]
        //    csharp_code += "}"
        //    return csharp_code
        public Dictionary<int, int> GaloisLog = Static.GaloisLog;
        public Dictionary<int, int> GaloisAntiLog = Static.GaloisAntiLog;
        public SupportedEncodingMode EncodingMode { get; set; }
        public ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
        public int Version { get; set; }

        public ReedSolomonEncoder(SupportedEncodingMode encodingMode, ErrorCorrectionLevels errorCorrectionLevel, int version)
        {
            EncodingMode = encodingMode;
            ErrorCorrectionLevel = errorCorrectionLevel;
            Version = version;
        }

        public string Encode(string Data)
        {
            //Step 5: Generate Powers of 2 using Byte-Wise Modulo 100011101
            // use byte-wise modulo 100011101 arithmetic.This means that when a number is 256 or larger, it should be XORed with 285.
            //2**8 = 256 ^ 285 = 29
            //Note that when continuing on to 29, do not take its usual value of 512 and XOR with 285(which would result in too large a number anyway).Instead, since 29 = 28 * 2, use the value of 28 that was calculated in the previous step.
            //2**9 = 28 * 2 = 29 * 2 = 58
            //Whenever a value greater than or equal to 256 is obtained, once again XOR with 285:
            //2**12 = 2**11 * 2 = 232 * 2 = 464 ^ 285 = 205

            // With optimisation :
            // 2**170 * 2**164 = 2**(170+164) = 2**334 → 2**(334 % 255) = 2**79
            return "";
        }

    }
}
