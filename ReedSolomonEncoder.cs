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

        public List<List<List<int>>> BreakUpDataIntoBlocks(string[] data, SupportedEncodingMode encodingMode, ErrorCorrectionLevels errorCorrectionLevel, int version)
        {
           // Data is split into bytes (8 bits)
           // Group Number	    Block Number	Data Codewords in the Group
           int ecWordPerBlock = ErrorCorrectionCodeWordCount[version][errorCorrectionLevel.ToString()][0];
           int blockGroup1 = ErrorCorrectionCodeWordCount[version][errorCorrectionLevel.ToString()][1];
           int wordCount1 = ErrorCorrectionCodeWordCount[version][errorCorrectionLevel.ToString()][2];
           int blockGroup2 = ErrorCorrectionCodeWordCount[version][errorCorrectionLevel.ToString()][3];
           int wordCount2 = ErrorCorrectionCodeWordCount[version][errorCorrectionLevel.ToString()][4];

           var blocks = new List<List<List<int>>>();
           // Cursor is used to keep track of the current position in the data array
           int cursor = 0;

           // for each group, 2 groups
           int group = 0;
           //  For each block in the group
           for (int blockGroup1Index = 0; blockGroup1Index < blockGroup1; blockGroup1Index++)
           {
               // For each word in the block
               for (int wordCount1Index = 0; wordCount1Index < wordCount1; wordCount1Index++)
               {
                   blocks[group][blockGroup1Index][wordCount1Index] = data[cursor];
                   cursor++;
               }
           }

           group = 1;
           // May be 0
           for (int blockGroup2Index = 0; blockGroup2Index < blockGroup2; blockGroup2Index++)
           {
               for (int wordCount2Index = 0; wordCount2Index < wordCount2; wordCount2Index++)
               {
                   blocks[group][blockGroup2Index][wordCount2Index] = data[cursor];
                   cursor++;
               }
           }
            

           // Display for debug:
           for(int i = 0; i < blocks.Count; i++)
           {
               for(int j = 0; j < blocks[i].Count; j++)
               {
                   for(int k = 0; k < blocks[i][j].Count; k++)
                   {
                       Console.WriteLine("Block " + i + " Group " + j + " Word " + k + " : " + blocks[i][j][k]);
                   }
               }
           }

           return blocks;

        }

    }
}
