using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;
using STH1123.ReedSolomon;

namespace QRGenerator
{
    public class QrErrorEncoder
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
        private static Dictionary<int, Dictionary<string, int[]>> ErrorCorrectionCodeWordCount = Static.ErrorCorrectionCodeWordCount;

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
        private SupportedEncodingMode EncodingMode { get; set; }
        private ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
        private int Version { get; set; }
        private int ECWordPerBlock { get; set; }
        public List<List<List<int>>> ErrorEncodedBlocks { get; set; }

        public QrErrorEncoder(SupportedEncodingMode encodingMode, ErrorCorrectionLevels errorCorrectionLevel, int version, string data)
        {
            EncodingMode = encodingMode;
            ErrorCorrectionLevel = errorCorrectionLevel;
            Version = version;

            List<List<List<string>>> Blocks = BreakUpDataIntoBlocks(data);
            List<List<List<int>>> BlocksAsNumbers = ConvertBlocksToInt(Blocks);
            DisplayForDebug(BlocksAsNumbers);

            var field = new GenericGF(285, 256, 0);
            var rse = new ReedSolomonEncoder(field);
            for (int i = 0; i < BlocksAsNumbers.Count; i++)
            {
                for (int j = 0; j < BlocksAsNumbers[i].Count; j++)
                {
                    int[] dataBlock = BlocksAsNumbers[i][j].ToArray();
                    rse.Encode(dataBlock, ECWordPerBlock);
                    BlocksAsNumbers[i][j] = dataBlock.ToList();
                }
            }
            Console.WriteLine("------------------------------------------------------------------\nAfter encoding\n------------------------------------------------------------------");
            DisplayForDebug(BlocksAsNumbers);
            ErrorEncodedBlocks = BlocksAsNumbers;
        }

        private List<List<List<string>>> BreakUpDataIntoBlocks(string Data)
        {
            //    split string into codewords :
            // "0100001101010101010001101000011001010111"
            // => [01000011, 01010101, 01000110, 10000110, 01010111]

            // split string into 8 bits :
            string[] stringCodewords = new string[Data.Length / 8];
            for (int i = 0; i < Data.Length; i += 8)
            {
                stringCodewords[i / 8] = Data.Substring(i, 8);
            }


            // Group Number	    Block Number	Data Codewords in the Group
            int ecWordPerBlock = ErrorCorrectionCodeWordCount[Version][ErrorCorrectionLevel.ToString()][0];
            ECWordPerBlock = ecWordPerBlock;
            int blockGroup1 = ErrorCorrectionCodeWordCount[Version][ErrorCorrectionLevel.ToString()][1];
            int wordCount1 = ErrorCorrectionCodeWordCount[Version][ErrorCorrectionLevel.ToString()][2];
            int blockGroup2 = ErrorCorrectionCodeWordCount[Version][ErrorCorrectionLevel.ToString()][3];
            int wordCount2 = ErrorCorrectionCodeWordCount[Version][ErrorCorrectionLevel.ToString()][4];

            // Initialize the blocks arrays
            var blocks = new List<List<List<string>>>
            {
                new List<List<string>>() { // Group 1
                    new List<string>(), // Block 1
                    new List<string>(), // Block 2
                }
            };
            if (blockGroup2 > 0)
            {
                blocks.Add(new List<List<string>>() { // Group 2
                    new List<string>(), // Block 1
                    new List<string>(), // Block 2
                });
            }


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
                    blocks[group][blockGroup1Index].Add(stringCodewords[cursor]);
                    cursor++;
                }
                // Add the error correction words
                for (int ecWordPerBlockIndex = 0; ecWordPerBlockIndex < ecWordPerBlock; ecWordPerBlockIndex++)
                {
                    blocks[group][blockGroup1Index].Add("00000000");
                }
            }

            group = 1;
            // May be 0
            for (int blockGroup2Index = 0; blockGroup2Index < blockGroup2; blockGroup2Index++)
            {
                for (int wordCount2Index = 0; wordCount2Index < wordCount2; wordCount2Index++)
                {
                    blocks[group][blockGroup2Index].Add(stringCodewords[cursor]);
                    cursor++;
                }
                for (int ecWordPerBlockIndex = 0; ecWordPerBlockIndex < ecWordPerBlock; ecWordPerBlockIndex++)
                {
                    blocks[group][blockGroup2Index].Add("00000000");
                }
            }

            return blocks;
        }

        /// <summary>
        /// Convert the blocks from string to bytes
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static List<List<List<int>>> ConvertBlocksToInt(List<List<List<string>>> blocks)
        {
            var blocksAsInt = new List<List<List<int>>>();
            for (int i = 0; i < blocks.Count; i++)
            {
                blocksAsInt.Add(new List<List<int>>());
                for (int j = 0; j < blocks[i].Count; j++)
                {
                    blocksAsInt[i].Add(new List<int>());
                    for (int k = 0; k < blocks[i][j].Count; k++)
                    {
                        // blocksAsBytes[i][j].Add(Convert.ToByte(blocks[i][j][k], 2));
                        blocksAsInt[i][j].Add(Convert.ToInt32(blocks[i][j][k], 2));
                    }
                }
            }
            return blocksAsInt;
        }

        public void DisplayForDebug(List<List<List<int>>> blocks)
        {

            // Display for debug:
            for (int i = 0; i < blocks.Count; i++)
            {
                for (int j = 0; j < blocks[i].Count; j++)
                {
                    for (int k = 0; k < blocks[i][j].Count; k++)
                    {
                        Console.WriteLine("Group " + i + " Block " + j + " Word " + k + " : " + blocks[i][j][k]);
                    }
                }
            }
        }
    }
}
